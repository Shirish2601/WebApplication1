using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reader
{
    public class FileReader : IDataReader
    {
        public void Read(string path)
        {
            if (path is not null)
            {
                List<string> machineList = new();
                List<Machine> machines = new();
                using (StreamReader sr = new StreamReader(path))
                {
                    string current = sr.ReadLine();
                    while (current != null)
                    {
                        current = sr.ReadLine();
                        string[] contents = current.Split(",");
                        var machineName = contents[0];
                        if (machineName is not null && !machineList.Contains(machineName))
                        {
                            var asset = new Asset
                            {
                                AssetName = contents[1],
                                SeriesNumber = contents[2]
                            };
                            var machine = new Machine
                            {
                                MachineName = machineName,
                                Assets = new() { asset }
                            };
                            machineList.Add(machineName);
                            machines.Add(machine);
                        }
                        else
                        {
                            var asset = new Asset
                            {
                                AssetName = contents[1],
                                SeriesNumber = contents[2]
                            };
                            var currentMachine = machines.Find(machine => machine.MachineName == machineName);
                            currentMachine.Assets.Add(asset);
                        }
                    }
                    foreach (var item in machines)
                    {
                        Console.Write($"{item.MachineName},");
                        foreach (var asset in item.Assets)
                        {
                            Console.WriteLine($"{asset.AssetName}, {asset.SeriesNumber}");
                        }
                    }

                    Console.WriteLine("--------------------------------------");
                    Console.ReadLine();
                }
            }
        }
    }
}
