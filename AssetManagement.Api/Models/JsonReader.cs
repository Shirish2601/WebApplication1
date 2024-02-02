using AssetManagement.Models;
using Newtonsoft.Json;
using System.IO;

namespace AssetManagement.Api.Models
{
    public class JsonReader : FileReader
    {
        public JsonReader(string path) : base(path)
        {
        }

        public override void Read()
        {
            List<Machine> machineList = new();

            List<MachineDto> machines = JsonConvert.DeserializeObject<List<MachineDto>>(File.ReadAllText(Path));
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
                    machineList.Add(Machine);
                }
                else
                {
                    var existingMachine = machineList.Find(machine => machine.MachineName == item.machineName);
                    var Asset = new Asset
                    {
                        AssetName = item.assetName,
                        SeriesNumber = item.seriesNumber
                    };
                    existingMachine.Assets.Add(Asset);
                }
            }
            AppConstants.Machines = machineList;
        }
        private class MachineDto
        {
            public string machineName {get; set;} = String.Empty;
            public string assetName {get; set;} = String.Empty;
            public string seriesNumber {get; set;} = String.Empty;
        }
    }
}
