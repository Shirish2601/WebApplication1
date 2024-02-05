﻿using AssetManagement.Models;
using Newtonsoft.Json;
using System.IO;

namespace AssetManagement.Api.Models
{
    public class JsonFileReader : FileReader
    {
        public JsonFileReader(string path) : base(path)
        {
        }

        public override void Read()
        {
            List<MachineDto>? temporaryMachineList = JsonConvert.DeserializeObject<List<MachineDto>>(File.ReadAllText(Path));
            List<Machine>? machineList = temporaryMachineList?.GroupBy(machine => machine.MachineName)
                .Select(group => new Machine { MachineName = group.Key, Assets = group.Select(asset => new Asset {AssetName = asset.AssetName, SeriesNumber = asset.SeriesNumber}).ToList()})
                .ToList();
            AppConstants.Machines = machineList;
        }
    }
}
