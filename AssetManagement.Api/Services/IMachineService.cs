using AssetManagement.Models;

namespace AssetManagement.Api.Services
{
    public interface IMachineService
    {
        public List<Machine> GetMachines();
        public List<Asset> GetAsset(string? machineName);
        public List<string> GetMachinesByAssetName(string? assetName);
        public List<string> GetMachineThatUsesLatestAssets();
    }
}
