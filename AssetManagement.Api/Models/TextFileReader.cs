﻿using AssetManagement.Models;
namespace AssetManagement.Api.Models
{
    public class TextFileReader : IFileReader
    {
        public List<Machine>? Machines { get; set; }
        public string Path { get; }

        public TextFileReader(string path)
        {
            Path = path;
        }

        public void Read()
        {
            List<MachineDto> temporaryMachineList = new();
            if (Path != null)
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    string? current = string.Empty;
                    while ((current = sr.ReadLine()) != null)
                    {
                        string[]? contents = current?.Split(",");
                        var machineName = contents?[0].Trim();
                        var assetName = contents?[1].Trim();
                        var seriesNumber = contents?[2].Trim();

                        temporaryMachineList.Add(new MachineDto() {AssetName = assetName, MachineName = machineName, SeriesNumber = seriesNumber});
                    }
                }
            }
            List<Machine> machineList = temporaryMachineList.GroupBy(machine => machine.MachineName)
                .Select(group => new Machine { MachineName = group.Key, Assets = group.Select(asset => new Asset {AssetName = asset.AssetName, SeriesNumber = asset.SeriesNumber}).ToList()})
                .ToList();

            Machines = machineList;
        }
    }
}
