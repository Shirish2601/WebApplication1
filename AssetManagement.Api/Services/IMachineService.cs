using AssetManagement.Models;

namespace AssetManagement.Api.Services
{
    public interface IMachineService
    {
        public List<Machine> GetMachines();
        public List<Asset> GetAssetsByMachineName(string? machineName);
        public List<string> GetMachineThatUsesLatestAssets();
        public List<string> GetMachinesByAssetAndSeries(string? assetName, string? seriesNumber);
    }
}
