﻿using AssetManagement.Models;
using Newtonsoft.Json;

namespace AssetManagement.Api.Models
{
    public class JsonReader : DataReader
    {
        public override void Read(string? path)
        {
            List<MachineDto> machines = JsonConvert.DeserializeObject<List<MachineDto>>(File.ReadAllText(path));
            List<string> existingMachineNames = new();

            foreach (var item in machines)
            {
                if (!existingMachineNames.Contains(item.machineName))
                {
                    existingMachineNames.Add(item.machineName);
                    var Asset = new Asset
                    {
                        AssetName = item.assetName ,
                        SeriesNumber = item.seriesNumber
                    };
                    var Machine = new Machine
                    {
                        MachineName = item.machineName,
                        Assets = new() { Asset }
                    };
                    Machines.Add(Machine);
                }
                else
                {
                    var existingMachine = Machines.Find(machine => machine.MachineName == item.machineName);
                    var Asset = new Asset
                    {
                        AssetName = item.assetName,
                        SeriesNumber = item.seriesNumber
                    };
                    existingMachine.Assets.Add(Asset);
                }
            }
        }
        public class MachineDto
        {
            public string machineName {get; set;} = String.Empty;
            public string assetName {get; set;} = String.Empty;
            public string seriesNumber {get; set;} = String.Empty;
        }
    }
}
