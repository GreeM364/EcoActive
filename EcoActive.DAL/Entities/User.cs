namespace EcoActive.DAL.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public FactoryAdmin? FactoryAdmin { get; set; }
        public Employee? Employee { get; set; }
        public Activist? Activist { get; set; }
    }
}
