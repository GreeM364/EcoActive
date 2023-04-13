using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface ILoginService
    {
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO request);
    }
}
