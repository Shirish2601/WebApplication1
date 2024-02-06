using AssetManagement.Api.MongoDB_Models;
using AssetManagement.Api.Repository;
using AssetManagement.Models;
using MongoDB.Driver;

namespace AssetManagement.Api.MongoDB
{
    public class MongoDbRepository : IMachineRepository
    {
        private readonly IMongoCollection<Machine> _machineCollection;
        public MongoDbRepository(IMachineDataStoreSetting machineDataStoreSetting, IMongoClient client)
        {
            var db = client.GetDatabase(machineDataStoreSetting.DatabaseName);
            _machineCollection = db.GetCollection<Machine>(machineDataStoreSetting.CollectionName);
        }

        public List<Asset> GetAsset(string? machineName)
        {
            var filterQuery = Builders<Machine>.Filter.Eq(machine => machine.MachineName, machineName);
            return _machineCollection.Find(filterQuery).Project(machine => machine.Assets).FirstOrDefault();
        }

        public List<Machine> GetMachines()
        {
            return _machineCollection.Find(machine => true).ToList();
        }
        public List<string> GetMachinesByAssetName(string? assetName)
        {
            var filterQuery = Builders<Machine>.Filter.ElemMatch(machine => machine.Assets, asset => asset.AssetName == assetName);
            return _machineCollection.Find(filterQuery).Project(machine => machine.MachineName).ToList();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            List<Machine> machineList = _machineCollection.Find(machine => true).ToList();

            Dictionary<string, int> assetDictionary = new();

            foreach (var machine in machineList)
            {
                foreach (var asset in machine.Assets)
                {
                    if (!assetDictionary.ContainsKey(machine.MachineName))
                    {
                        assetDictionary.Add(asset.AssetName, Convert.ToInt32(asset.SeriesNumber.Substring(1)));
                    }
                    else
                    {
                        var currentSeriesNumber = Convert.ToInt32(asset.SeriesNumber.Substring(1));
                        var currentSeriesNumberFromDictionary = assetDictionary[asset.AssetName];

                        assetDictionary[asset.AssetName] = Math.Max(currentSeriesNumber, currentSeriesNumberFromDictionary);
                    }
                }
            }

            List<string> machineThatUsesLatestAsset = new();

            foreach (var machine in machineList)
            {
                bool found = true;
                foreach (var asset in machine.Assets)
                {
                    var currentAssetSeriesNumber = Convert.ToInt32(asset.SeriesNumber.Substring(1));
                    var maximumSeriesNumberOfCurrentAsset = assetDictionary[asset.AssetName];

                    if (currentAssetSeriesNumber != maximumSeriesNumberOfCurrentAsset)
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    machineThatUsesLatestAsset.Add(machine.MachineName);
                }
            }
            return machineThatUsesLatestAsset;
        }
    }
}
