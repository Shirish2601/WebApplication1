using MachineManagement.Models;

namespace MachineManagement.Web.Services
{
    public class MachineService : IMachineService
    {
        private readonly HttpClient _httpClient;

        public MachineService(HttpClient httpClient) 
        {
            _httpClient = httpClient;    
        }

        public async Task<Asset> GetAssets(string? machineName)
        {
            return await _httpClient.GetFromJsonAsync<Asset>($"api/Machine/{machineName}/assets");
        }

        public async Task<Dictionary<string, List<Asset>>> GetAssets()
        {
            return await _httpClient.GetFromJsonAsync<Dictionary<string, List<Asset>>>($"api/Machine");
        }
    }
}
