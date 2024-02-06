using AssetManagement.Api.MongoDB_Models;
using AssetManagement.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AssetManagement.Api.MongoDB
{
    public class MongoDbReader
    {
        private readonly IMongoCollection<Machine> _machineCollection;
        public MongoDbReader(IMachineDataStoreSetting machineDataStoreSetting, IMongoClient client)
        {
            var db = client.GetDatabase(machineDataStoreSetting.DatabaseName);
            _machineCollection = db.GetCollection<Machine>(machineDataStoreSetting.CollectionName);
            
            var countOfDocumentsInMachineCollection = _machineCollection.CountDocuments(machine => true);
            if (countOfDocumentsInMachineCollection == 0)
            {
                List<Machine> Machines = new()
                {
                    new Machine()
                    {
                        MachineName = "C300",
                        Assets = new List<Asset>()
                        {
                            new Asset() { AssetName = "Cutter head", SeriesNumber = "S6" },
                            new Asset() { AssetName = "Blade safety cover", SeriesNumber = "S10" },
                            new Asset() { AssetName = "Deburring blades", SeriesNumber = "S6" }
                        }
                    },
                    new Machine()
                    {
                        MachineName = "C40",
                        Assets = new List<Asset>()
                        {
                            new Asset() { AssetName = "Cutter head", SeriesNumber = "S7" },
                            new Asset() { AssetName = "Blade safety cover", SeriesNumber = "S11" },
                            new Asset() { AssetName = "Shutter gripper", SeriesNumber = "S3" }
                        }
                    },
                    new Machine()
                    {
                        MachineName = "C60",
                        Assets = new List<Asset>()
                        {
                            new Asset() { AssetName = "Blade safety cover", SeriesNumber = "S11" },
                            new Asset() { AssetName = "Cutter head", SeriesNumber = "S8" },
                            new Asset() { AssetName = "Clamping fixture", SeriesNumber = "S2" }
                        }
                    }
                };

            }
        }
    }
}