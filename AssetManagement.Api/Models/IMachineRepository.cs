using AssetManagement.Models;

namespace AssetManagement.Api.Models
{
    public interface IMachineRepository
    {
        public List<Machine> GetMachines(); 
        public List<Asset> GetAsset(string? machineName); 
        public List<string> GetMachines(string? assetName);
        public List<string> GetMachineThatUsesLatestAssets();
    }
}
