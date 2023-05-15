using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface IActivistService
    {
        Task<ActivistDTO> GetByIdAsync(string id);
        Task<List<ActivistDTO>> GetAsync();
        Task<ActivistDTO> CreateAsync(ActivistCreateDTO request);
        Task<ActivistDTO> UpdateAsync(string id, ActivistUpdateDTO request);
        Task DeleteAsync(string id);
        Task<List<FactoryDTO>> GetFactoriesAsync(string id);
        Task AddFactoryToActivistAsync(string activistId, AddFactoryToActivistDTO request);
        Task DeleteFactoryToActivistAsync(string factoryId);
    }
}
