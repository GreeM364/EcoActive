using EcoActive.DAL.Entities;
using System.Linq.Expressions;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                                  IOrderedQueryable<T>>? orderBy = null, string includeProperties = null,
                                  bool isTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = null, bool isTracking = true);

        Task<T> GetByIdAsync(string id);
        Task RemoveAsync(T entity);
        Task RemoveRange(IEnumerable<T> entity);
        Task SaveAsync();
    }
}
