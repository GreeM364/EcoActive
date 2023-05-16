namespace EcoActive.API.Models
{
    public class CriticalIndicatorsViewModel
    {
        public string Id { get; set; } = null!;
        public string FactoryId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}
