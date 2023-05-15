namespace EcoActive.BLL.DataTransferObjects
{
    public class FactoryCreateDTO
    {
        public string? ActivistId { get; set; }
        public string Name { get; set; } = null!;
        public string Territory { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
