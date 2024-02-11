using AssetManagement.Models;

namespace AssetManagement.Api.Models
{
    public interface IFileReader
    {
        public List<Machine>? Machines { get; set; }
        public string? Path { get; }

        void Read();
    }
}
