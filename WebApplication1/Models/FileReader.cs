namespace WebApplication1.Models
{
    public class FileReader : DataReader
    {
        public override void Read(string? path)
        {
            if (path != null)
            {
                List<string> machineList = new();
                using (StreamReader sr = new StreamReader(path))
                {
                    string? current = string.Empty;
                    while ((current = sr.ReadLine()) != null)
                    {
                        string[]? contents = current?.Split(",");
                        var machineName = contents?[0].Trim();
                        if (machineName is not null && !machineList.Contains(machineName))
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
                            machineList.Add(machineName);
                            Machines.Add(machine);
                        }
                        else
                        {
                            var asset = new Asset
                            {
                                AssetName = contents?[1].Trim(),
                                SeriesNumber = contents?[2].Trim()
                            };
                            var currentMachine = Machines.Find(machine => machine.MachineName == machineName);
                            currentMachine?.Assets.Add(asset);
                        }
                    }
                }
            }
        }
    }
}
