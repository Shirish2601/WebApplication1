namespace WebApplication1.Models
{
    public class FileReader : DataReader
    {
        public override void Read(string? path)
        {
            if (path is not null)
            {
                List<string> machineList = new();
                using (StreamReader sr = new StreamReader(path))
                {
                    string? current = sr.ReadLine();
                    while (current != null)
                    {
                        current = sr.ReadLine();
                        string[]? contents = current?.Split(",");
                        var machineName = contents?[0];
                        if (machineName is not null && !machineList.Contains(machineName))
                        {
                            var asset = new Asset
                            {
                                AssetName = contents?[1],
                                SeriesNumber = contents?[2]
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
                                AssetName = contents?[1],
                                SeriesNumber = contents?[2]
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
