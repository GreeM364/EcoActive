namespace EcoActive.BLL.DataTransferObjects
{
    public class ProfileDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }


        public FactoryAdministratorDTO? FactoryAdmin { get; set; }
        public EmployeeDTO? Employee { get; set; }
        public ActivistDTO? Activist { get; set; }
    }
}
