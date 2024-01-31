namespace Models
{
    public class Machine
    {
        public string? MachineName { get; set; }  = string.Empty;
        public List<Asset>? Assets { get; set; }
    }
}