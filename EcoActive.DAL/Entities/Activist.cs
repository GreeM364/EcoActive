namespace EcoActive.DAL.Entities
{
    public class Activist : BaseModel
    {
        public string PublicOrganization { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
