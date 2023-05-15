namespace EcoActive.DAL.Entities
{
    public class Factory : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Territory { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime DataPaySubscription { get; set; }

        public string? ActivistId { get; set; }
        public Activist? Activist { get; set; }
         
        public List<Employee>? Employees { get; set; }
        public List<FactoryAdmin>? FactoryAdmins { get; set; }
        public List<EnvironmentalIndicators>? EnvironmentalIndicators { get; set; }
        public List<CriticalIndicators>? CriticalIndicators { get; set; }
    }
}
