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
            entity.DataPaySubscription = DateTime.Today;

            _db.Factories.Add(entity);

            await _db.SaveChangesAsync();
        }
    }
}
