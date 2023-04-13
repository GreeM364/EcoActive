namespace EcoActive.DAL.Entities
{
    public class Factory : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Territory { get; set; } = null!;
        public string Type { get; set; } = null!;
         
        public List<Employee>? FactoryEmployees { get; set; }
        public List<FactoryAdmin>? FactoryAdmins { get; set; }
    }
}
