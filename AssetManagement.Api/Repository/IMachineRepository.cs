using AssetManagement.Models;

namespace AssetManagement.Api.Repository
{
    public interface IMachineRepository
    {
        public List<Machine> GetMachines();
        public List<Asset> GetAssetsByMachineName(string? machineName);
        public List<string> GetMachineThatUsesLatestAssets();
        public List<string> GetMachinesByAssetAndSeries(string? assetName, string? seriesNumber);
    }
}
