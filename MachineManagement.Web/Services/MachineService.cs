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

        public async Task<List<Machine>> GetMachines()
        {
            return await _httpClient.GetFromJsonAsync<List<Machine>>($"api/Machine");
        }
    }
}
