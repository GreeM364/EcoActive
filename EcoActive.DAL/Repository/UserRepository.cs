using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcoActive.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcoActiveDbContext _db;

        public UserRepository(EcoActiveDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetAsync(string id, string includeProperties = null)
        {
            IQueryable<User> query = _db.BaseUsers;

            query = query.Where(x => x.Id == id);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _db.BaseUsers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
