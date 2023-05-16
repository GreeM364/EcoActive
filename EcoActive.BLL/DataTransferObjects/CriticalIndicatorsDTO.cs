namespace EcoActive.BLL.DataTransferObjects
{
    public class CriticalIndicatorsDTO
    {
        public string Id { get; set; } = null!;
        public string FactoryId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}
