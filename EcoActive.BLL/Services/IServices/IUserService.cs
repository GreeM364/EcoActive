using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface IUserService
    {
        Task<ProfileDTO> GetProfileAsync(string userId);
    }
}
