using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IFactoryRepository : IRepository<Factory>
    {
        Task CreateAsync(Factory entity);
        Task<Factory> UpdateAsync(Factory entity);
    }
}
