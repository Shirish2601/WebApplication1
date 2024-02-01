namespace WebApplication1.Models
{
    public class MachineRepository : IMachineRepository
    {
        public DataReader DataReader;
        public MachineRepository()
        {
            DataReader = new FileReader();
            DataReader.Read(@"C:\Users\Hadp_shi\Desktop\Shirish\New folder\WebApplication1\Models\Matrix.txt");
        }

        public List<Asset> GetAsset(string? machineName)
        {
            return DataReader.Machines.Where(machine => machine.MachineName == machineName).First().Assets;
        }

        public Dictionary<string, List<Asset>> GetAssets()
        {
            Dictionary<string, List<Asset>> assetList = new();
            foreach (var machine in DataReader.Machines)
            {
                if (machine.MachineName != null &&  !assetList.ContainsKey(machine.MachineName))
                {
                    assetList.Add(machine.MachineName,machine.Assets);
                }
            }
            return assetList;
        }

        public List<string> GetMachines(string? assetName)
        {
            return DataReader.Machines.Where(machine => machine.Assets.Any(asset => asset.AssetName.ToLower() == assetName.ToLower())).Select(machine => machine.MachineName).ToList();
        }

        public List<string> GetMachineThatUsesLatestAssets()
        {
            Dictionary<string, int> assetDictionary = new();
            foreach (var machine in DataReader.Machines)
            {
                foreach (var asset in machine.Assets)
                {
                    if (!assetDictionary.ContainsKey(asset.AssetName))
                    {
                        assetDictionary.Add(asset.AssetName, Convert.ToInt32(asset.SeriesNumber.Substring(1)));
                    }
                    else
                    {
                        int currentSeriesNumber = Convert.ToInt32(asset.SeriesNumber.Substring(1));
                        int currentDictionarySeriesNumber = assetDictionary[asset.AssetName];

                        assetDictionary[asset.AssetName] = Math.Max(currentSeriesNumber, currentDictionarySeriesNumber);
                    }
                }
            }

            List<string> machineThatUsesLatestAssets = new();
            foreach (var machine in DataReader.Machines)
            {
                bool found = true;

                foreach (var asset in machine.Assets)
                {
                   if (Convert.ToInt32(asset.SeriesNumber.Substring(1)) != assetDictionary[asset.AssetName])
                   {
                        found = false;
                        break;
                   }
                }
                if (found)
                {
                    machineThatUsesLatestAssets.Add(machine.MachineName);
                }
            }
            return machineThatUsesLatestAssets;
        }
    }
}
