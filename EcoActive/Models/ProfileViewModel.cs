namespace EcoActive.API.Models
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }


        public FactoryAdministratorViewModel? FactoryAdmin { get; set; }
        public EmployeeViewModel? Employee { get; set; }
        public ActivistViewModel? Activist { get; set; }
    }
}
