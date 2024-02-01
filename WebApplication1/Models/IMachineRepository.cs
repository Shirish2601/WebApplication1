namespace WebApplication1.Models
{
    public interface IMachineRepository
    {
        public Dictionary<string, List<Asset>> GetAssets(); 
        public List<Asset> GetAsset(string? machineName); 
        public List<string> GetMachines(string? assetName);
        public List<string> GetMachineThatUsesLatestAssets();
    }
}
