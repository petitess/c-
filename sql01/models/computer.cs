namespace Computer.Models
{
    public class Computer
    {
        public string? Motherboard { get; set; }
        public int CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public string? VideoCard { get; set; }

    }
}