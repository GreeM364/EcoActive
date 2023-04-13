using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IFactoryAdminRepository : IRepository<FactoryAdmin>
    {
        Task CreateAsync(FactoryAdmin entity, string password);
        Task<FactoryAdmin> UpdateAsync(FactoryAdmin entity);
    }
}
