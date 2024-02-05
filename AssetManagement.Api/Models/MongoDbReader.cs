using AssetManagement.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AssetManagement.Api.Models
{
    public class MongoDbReader
    {
        private readonly IMongoCollection<Machine> _machineCollection;
        public MongoDbReader(IMachineDataStoreSetting machineDataStoreSetting, IMongoClient client)
        {
            var db = client.GetDatabase(machineDataStoreSetting.DatabaseName);
            _machineCollection = db.GetCollection<Machine>(machineDataStoreSetting.CollectionName);
        }
        public void Read()
        {
            var iterableMachineList = _machineCollection.Find(new BsonDocument()).ToList();
        }
    }
}
