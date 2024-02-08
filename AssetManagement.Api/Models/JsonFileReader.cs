using AssetManagement.Models;
using Newtonsoft.Json;

namespace AssetManagement.Api.Models
{
    public class JsonFileReader : IFileReader
    {
        public string Path { get; }
        public JsonFileReader(string path) 
        {
            Path = path;
        }

        public void Read()
        {
            List<MachineDto>? temporaryMachineList = JsonConvert.DeserializeObject<List<MachineDto>>(File.ReadAllText(Path));

            List<Machine>? machineList = temporaryMachineList?.GroupBy(machine => machine.MachineName)
                .Select(group => new Machine { MachineName = group.Key, Assets = group.Select(asset => new Asset {AssetName = asset.AssetName, SeriesNumber = asset.SeriesNumber}).ToList()})
                .ToList();

            AppConstants.Machines = machineList;
        }
    }
}
