namespace EcoActive.DAL.Entities
{
    public class FactoryEmployee : BaseModel
    {
        public DateTime BeginningWorkingDay { get; set; }
        public DateTime EndWorkingDay { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string FactoryId { get; set; } = null!;
        public Factory Factory { get; set; } = null!;
    }
}
