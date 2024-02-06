namespace AssetManagement.Api.MongoDBModels
{
    public class MachineDataStoreSetting : IMachineDataStoreSetting
    {
        public string MongoConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
    }
}
