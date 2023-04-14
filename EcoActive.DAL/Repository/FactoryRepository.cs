using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.DAL.Repository
{
    public class FactoryRepository : Repository<Factory>, IFactoryRepository
    {
        private readonly EcoActiveDbContext _db;
        public FactoryRepository(EcoActiveDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task CreateAsync(Factory entity)
        {
            entity.CreatedDate = DateTime.Now;

            _db.Factories.Add(entity);

            await SaveAsync();
        }

        public async Task<Factory> UpdateAsync(Factory entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.Factories.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
