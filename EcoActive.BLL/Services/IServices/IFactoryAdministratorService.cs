using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface IFactoryAdministratorService
    {
        public Task<FactoryAdministratorDTO> GetByIdAsync(string id);
        public Task<List<FactoryAdministratorDTO>> GetAsync();
        public Task<FactoryAdministratorDTO> CreateAsync(FactoryAdministratorCreateDTO request);
        public Task<FactoryAdministratorDTO> UpdateAsync(string id, FactoryAdministratorUpdateDTO request);
        public Task DeleteAsync(string id);
    }
}
