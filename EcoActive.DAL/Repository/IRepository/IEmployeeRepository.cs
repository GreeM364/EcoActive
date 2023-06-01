using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task CreateAsync(Employee entity, string password);
    }
}
