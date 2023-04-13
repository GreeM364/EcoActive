namespace EcoActive.BLL.DataTransferObjects
{
    public class ActivistDTO
    {
        public string Id { get; set; } = null!;
        public string PublicOrganization { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}
