
using AssetManagement.Models;

namespace AssetManagement.Api.Models
{
    public abstract class DataReader
    {
        public List<Machine> Machines { get; set; } = new();
        public abstract void Read(string? path);
    }
}
