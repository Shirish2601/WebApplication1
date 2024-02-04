using AssetManagement.Models;

namespace AssetManagement.Api.Models
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IDataReader _dataReader;
        
        public MachineRepository(IDataReader dataReader)
        {
            _dataReader = dataReader;
            _dataReader.Read();
        }

        public List<Asset> GetAsset(string? machineName)
        {
            return AppConstants.Machines.Where(machine => machine.MachineName?.ToLower() == machineName?.ToLower()).First().Assets;
        }

        public List<Machine> GetMachines()
        {
            return AppConstants.Machines;
        }

        public List<string> GetMachinesByAssetName(string? assetName)
        {
            return AppConstants.Machines.Where(machine => machine.Assets.Any(asset => asset.AssetName?.ToLower() == assetName?.ToLower())).Select(machine => machine.MachineName).ToList();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            Dictionary<string, int> assetDictionary = new();
            foreach (var machine in AppConstants.Machines)
            {
                foreach (var asset in machine.Assets)
                {
                    if (asset.AssetName!= null && !assetDictionary.ContainsKey(asset.AssetName))
                    {
                        assetDictionary.Add(asset.AssetName, Convert.ToInt32(asset.SeriesNumber?.Substring(1)));
                    }
                    else
                    {
                        int currentSeriesNumber = Convert.ToInt32(asset.SeriesNumber?.Substring(1));
                        int currentDictionarySeriesNumber = assetDictionary[asset.AssetName];

                        assetDictionary[asset.AssetName] = Math.Max(currentSeriesNumber, currentDictionarySeriesNumber);
                    }
                }
            }

            List<string> machineThatUsesLatestAssets = new();
            foreach (var machine in AppConstants.Machines)
            {
                bool found = true;

                foreach (var asset in machine.Assets)
                {
                   if (asset.AssetName != null && Convert.ToInt32(asset.SeriesNumber?.Substring(1)) != assetDictionary[asset.AssetName])
                   {
                        found = false;
                        break;
                   }
                }
                if (found && machine.MachineName != null)
                {
                    machineThatUsesLatestAssets.Add(machine.MachineName);
                }
            }
            return machineThatUsesLatestAssets;
        }
    }
}
