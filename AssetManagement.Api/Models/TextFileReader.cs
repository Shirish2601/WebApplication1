using AssetManagement.Models;
namespace AssetManagement.Api.Models
{
    public class TextFileReader : FileReader
    {
        public TextFileReader(string path) : base(path) { }

        public override void Read()
        {
            List<Machine> machineList = new();
            if (Path != null)
            {
                List<string> existingMachineNames = new();
                using (StreamReader sr = new StreamReader(Path))
                {
                    string? current = string.Empty;
                    while ((current = sr.ReadLine()) != null)
                    {
                        string[]? contents = current?.Split(",");
                        var machineName = contents?[0].Trim();
                        if (machineName is not null && !existingMachineNames.Contains(machineName))
                        {
                            var asset = new Asset
                            {
                                AssetName = contents?[1].Trim(),
                                SeriesNumber = contents?[2].Trim()
                            };
                            var machine = new Machine
                            {
                                MachineName = machineName,
                                Assets = new() { asset }
                            };
                            existingMachineNames.Add(machineName);
                            machineList.Add(machine);
                        }
                        else
                        {
                            var asset = new Asset
                            {
                                AssetName = contents?[1].Trim(),
                                SeriesNumber = contents?[2].Trim()
                            };
                            var currentMachine = machineList.Find(machine => machine.MachineName == machineName);
                            currentMachine?.Assets.Add(asset);
                        }
                    }
                }
            }
            AppConstants.Machines = machineList;
        }
    }
}
