using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.DAL.Repository
{
    public class EnvironmentalIndicatorsRepository : Repository<EnvironmentalIndicators>, IEnvironmentalIndicatorsRepository
    {
        private readonly EcoActiveDbContext _db;
        public EnvironmentalIndicatorsRepository(EcoActiveDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(EnvironmentalIndicators entity)
        {
            entity.CreatedDate = DateTime.Now;

            _db.EnvironmentalIndicators.Add(entity);

            await _db.SaveChangesAsync();
        }
    }
}
