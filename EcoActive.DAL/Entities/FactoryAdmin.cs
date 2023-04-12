namespace EcoActive.DAL.Entities
{
    public class FactoryAdmin : BaseModel
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public string FactoryId { get; set; } = null!;
        public Factory Factory { get; set; } = null!;
    }
}
