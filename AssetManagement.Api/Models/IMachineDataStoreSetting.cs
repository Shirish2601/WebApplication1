namespace AssetManagement.Api.Models
{
    public interface IMachineDataStoreSetting
    {
        public string MongoConnectionString { get; set; }
        public string DatabaseName { get; set; }    
        public string CollectionName { get; set; }
    }
}
