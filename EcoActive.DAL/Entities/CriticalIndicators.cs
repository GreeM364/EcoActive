namespace EcoActive.DAL.Entities
{
    public class CriticalIndicators : BaseModel
    {
        public string Message { get; set; } = null!;

        public string FactoryId { get; set; } = null!;
        public Factory Factory { get; set; } = null!;
    }
}
