using AssetManagement.Api.Models;
using AssetManagement.Models;

namespace AssetManagement.Api.Repository
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IFileReader _dataReader;

        public MachineRepository(IFileReader dataReader)
        {
            _dataReader = dataReader;
            _dataReader.Read();
        }

        public List<Asset> GetAssetsByMachineName(string? machineName)
        {
            return AppConstants.Machines.Where(machine => machine.MachineName?.ToLower() == machineName?.ToLower())
                .First().Assets;
        }

        public List<Machine> GetMachines()
        {
            return AppConstants.Machines;
        }

        public List<string> GetMachinesByAssetName(string? assetName)
        {
            return AppConstants.Machines.Where(machine => machine.Assets.Any(asset => asset.AssetName?.ToLower() == assetName?.ToLower()))
                .Select(machine => machine.MachineName)
                .ToList();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            Dictionary<string, int> assetDictionary = new();
            AppConstants.Machines.ForEach(m => { 
                m.Assets.ForEach(a =>
                {
                    if (a.AssetName != null && !assetDictionary.ContainsKey(a.AssetName))
                    {
                        assetDictionary.Add(a.AssetName, Convert.ToInt32(a.SeriesNumber?.Substring(1)));
                    }
                    else
                    {
                        int currentSeriesNumber = Convert.ToInt32(a.SeriesNumber?.Substring(1));
                        int currentDictionarySeriesNumber = assetDictionary[a.AssetName];

                        assetDictionary[a.AssetName] = Math.Max(currentSeriesNumber, currentDictionarySeriesNumber);
                    }
                });
            });
            var machinesThatUsesLatestAssets = AppConstants.Machines.FindAll(m => m.Assets.All(a => assetDictionary.ContainsKey(a.AssetName) && assetDictionary[a.AssetName] == int.Parse(a.SeriesNumber.Substring(1))))
                .Select(m => m.MachineName)
                .ToList();
            return machinesThatUsesLatestAssets;
        }
    }
}
