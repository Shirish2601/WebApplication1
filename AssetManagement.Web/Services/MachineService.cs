using AssetManagement.Models;

namespace AssetManagement.Web.Services
{
    public class MachineService : IMachineService
    {
        private readonly HttpClient _httpClient;

        public MachineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Asset>> GetAssets(string? machineName)
        {
            return await _httpClient.GetFromJsonAsync<List<Asset>>($"api/Machine/{machineName}/assets");
        }

        public async Task<List<Machine>> GetMachines()
        {
            return await _httpClient.GetFromJsonAsync<List<Machine>>($"api/Machine");
        }

        public async Task<List<string>> GetMachinesByAssetName(string? assetName)
        {
            if (assetName != null)
                return await _httpClient.GetFromJsonAsync<List<string>>($"api/Machine/{assetName}/machine");
            else
                return new();
        }

        public async Task<List<string>> GetMachineThatUsesLatestAsset()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"api/Machine/latest-asset");
        }
    }
}
