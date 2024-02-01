using AssetManagement.Models;

namespace AssetManagement.Web.Services
{
    public interface IMachineService
    {
        Task<Asset> GetAssets(string? machineName);
        Task<List<Machine>> GetMachines();
    }
}
