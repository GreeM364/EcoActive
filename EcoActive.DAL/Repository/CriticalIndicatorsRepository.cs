using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.DAL.Repository
{
    public class CriticalIndicatorsRepository : Repository<CriticalIndicators>, ICriticalIndicatorsRepository
    {
        private readonly EcoActiveDbContext _db;
        public CriticalIndicatorsRepository(EcoActiveDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateAsync(CriticalIndicators entity)
        {
            entity.CreatedDate = DateTime.Now;

            _db.CriticalIndicators.Add(entity);

            await _db.SaveChangesAsync();
        }

        public async Task<CriticalIndicators> UpdateAsync(CriticalIndicators entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.CriticalIndicators.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
