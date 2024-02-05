namespace AssetManagement.Api.Models
{
    public class MachineDataStoreSetting : IMachineDataStoreSetting
    {
        public string MongoConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
    }
}
