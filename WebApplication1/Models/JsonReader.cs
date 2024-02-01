using Newtonsoft.Json;

namespace MachineManagement.Api.Models
{
    public class JsonReader : DataReader
    {
        public override void Read(string? path)
        {
            List<MachineDto> machines = JsonConvert.DeserializeObject<List<MachineDto>>(File.ReadAllText(path));
        }
        public class MachineDto
        {
            public string MachineName {get; set;}
            public string AssetName {get; set;}
            public string SeriesNumber {get; set;}
        }
    }
}
