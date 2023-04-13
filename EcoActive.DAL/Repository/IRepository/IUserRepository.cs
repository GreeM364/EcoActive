using EcoActive.DAL.Entities;

namespace EcoActive.DAL.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);

        Task<User> GetAsync(string id, string includeProperties = null);
    }
}
