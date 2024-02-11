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

        public List<Asset> GetAssetsByMachineName(string? machineName)
        {
            if (machineName == null)
            {
                throw new ArgumentNullException(nameof(machineName));
            }
            return _mongoDbRepository.GetAssetsByMachineName(machineName);
        }

        public List<Machine> GetMachines()
        {
            return _mongoDbRepository.GetMachines();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            return _mongoDbRepository.GetMachineThatUsesLatestAssets();
        }

        public List<string> GetMachinesByAssetAndSeries(string? assetName, string? seriesNumber)
        {
            return _mongoDbRepository.GetMachinesByAssetAndSeries(assetName, seriesNumber);
        }
    }
}
