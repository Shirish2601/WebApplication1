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
            if (machineName is not null)
                return DataReader.Machines.Where(machine => machine.MachineName == machineName).First().Assets;
        }

        public Dictionary<string, List<Asset>> GetAssets()
        {
            Dictionary<string, List<Asset>> assetList = new();
            foreach (var machine in DataReader.Machines)
            {
                if (!assetList.ContainsKey(machine?.MachineName))
                {
                    assetList.Add(machine?.MachineName,machine?.Assets);
                }
            }
            return assetList;
        }
    }
}
