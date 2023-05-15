using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IEnvironmentalIndicatorsRepository : IRepository<EnvironmentalIndicators>
    {
        Task CreateAsync(EnvironmentalIndicators entity);
        Task<EnvironmentalIndicators> UpdateAsync(EnvironmentalIndicators entity);
    }
}
