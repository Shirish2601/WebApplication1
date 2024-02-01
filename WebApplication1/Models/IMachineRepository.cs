namespace WebApplication1.Models
{
    public interface IMachineRepository
    {
        public Dictionary<string, List<Asset>> GetAssets(); 
        public List<Asset> GetAsset(string? machineName); 
    }
}
