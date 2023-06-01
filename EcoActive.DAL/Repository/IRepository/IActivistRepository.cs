using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IActivistRepository : IRepository<Activist>
    {
        Task CreateAsync(Activist entity, string password);
    }
}
