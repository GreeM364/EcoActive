namespace EcoActive.IoT.Models
{
    public class CriticalEnvironmentalIndicators
    {
        public string FactoryId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
