using AssetManagement.Api.Models;
using AssetManagement.Models;
using MongoDB.Driver;

namespace AssetManagement.Api.Repository
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
            return _machineCollection.Find(filterQuery).Project(machine =>  machine.Assets).FirstOrDefault();
        }

        public List<Machine> GetMachines()
        {
            return _machineCollection.Find(machine => true).ToList();
        }
        public List<string> GetMachinesByAssetName(string? assetName)
        {
            var filterQuery = Builders<Machine>.Filter.ElemMatch(machine => machine.Assets, asset => asset.AssetName == assetName);
            return  _machineCollection.Find(filterQuery).Project(machine => machine.MachineName).ToList();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            return new();
        }
    }
}
