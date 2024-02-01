using MachineManagement.Models;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace MachineManagement.Web.Services
{
    public interface IMachineService
    {
        Task<Asset> GetAssets(string? machineName);
        Task<Dictionary<string, List<Asset>>> GetAssets();
    }
}
