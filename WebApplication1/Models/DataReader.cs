namespace WebApplication1.Models
{
    public abstract class DataReader
    {
        public List<Machine> Machines { get; set; } = new();
        public abstract void Read(string? path);
    }
}
