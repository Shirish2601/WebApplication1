using AssetManagement.Api.Repository;
using AssetManagement.Models;

namespace AssetManagement.Api.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
            
        }
        public List<Asset> GetAssetsByMachineName(string? machineName)
        {
            if (machineName == null)
            {
                throw new ArgumentNullException(nameof(machineName));
            }
            return _machineRepository.GetAssetsByMachineName(machineName);
        }

        public List<Machine> GetMachines()
        {
            return _machineRepository.GetMachines();
        }

        public List<string> GetMachinesByAssetName(string? assetName)
        {
            if (assetName == null)
            {
                throw new ArgumentNullException(nameof(assetName));
            }
            return _machineRepository.GetMachinesByAssetName(assetName);
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            return _machineRepository.GetMachineThatUsesLatestAssets();
        }
    }
}
