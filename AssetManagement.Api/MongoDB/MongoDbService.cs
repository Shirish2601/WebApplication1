using AssetManagement.Api.Repository;
using AssetManagement.Api.Services;
using AssetManagement.Models;

namespace AssetManagement.Api.MongoDB
{
    public class MongoDbService : IMachineService
    {
        private readonly IMachineRepository _mongoDbRepository;
        public MongoDbService(IMachineRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public List<Asset> GetAsset(string? machineName)
        {
            if (machineName == null)
            {
                throw new ArgumentNullException(nameof(machineName));
            }
            return _mongoDbRepository.GetAsset(machineName);
        }

        public List<Machine> GetMachines()
        {
            return _mongoDbRepository.GetMachines();
        }

        public List<string> GetMachinesByAssetName(string? assetName)
        {
            if (assetName == null)
            {
                throw new ArgumentNullException(nameof(assetName));
            }
            return _mongoDbRepository.GetMachinesByAssetName(assetName);
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            return _mongoDbRepository.GetMachineThatUsesLatestAssets();
        }
    }
}
