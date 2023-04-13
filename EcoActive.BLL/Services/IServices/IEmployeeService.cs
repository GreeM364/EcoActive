using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> GetByIdAsync(string id);
        Task<List<EmployeeDTO>> GetAsync();
        Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO request);
        Task<EmployeeDTO> UpdateAsync(string id, EmployeeUpdateDTO request);
        Task DeleteAsync(string id);
    }
}
