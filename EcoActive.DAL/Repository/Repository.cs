using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EcoActive.DAL.Data;


namespace EcoActive.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly EcoActiveDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(EcoActiveDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null,
                                      bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (!isTracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                                               IOrderedQueryable<T>>? orderBy = null, string includeProperties = null,
                                               bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return query.ToList();
        }

        public virtual Task<T> GetByIdAsync(string id)
        {
            return dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
