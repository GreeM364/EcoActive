namespace EcoActive.BLL.DataTransferObjects
{
    public class FactoryDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Territory { get; set; } = null!;
        public string Type { get; set; } = null!;

        public int EmployeesCount { get; set; }
        public int FactoryAdminsCount { get; set; }
    }
}
