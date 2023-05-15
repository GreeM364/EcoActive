using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface ICriticalIndicatorsRepository : IRepository<CriticalIndicators>
    {
        Task CreateAsync(CriticalIndicators entity);
        Task<CriticalIndicators> UpdateAsync(CriticalIndicators entity);
    }
}
