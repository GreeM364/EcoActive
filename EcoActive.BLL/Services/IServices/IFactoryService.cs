using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface IFactoryService
    {
        public Task<FactoryDTO> GetByIdAsync(string id);
        public Task<List<FactoryDTO>> GetAsync();
        public Task<FactoryDTO> CreateAsync(FactoryCreateDTO request);
        public Task<FactoryDTO> UpdateAsync(string id, FactoryUpdateDTO request);
        public Task DeleteAsync(string id);
        public Task<List<EmployeeDTO>> GetEmployeesAsync(string id);
        public Task<List<FactoryAdministratorDTO>> GetFactoryAdministratorsAsync(string id);
        public Task<ActivistDTO> GetActivistAsync(string id);
    }
}
