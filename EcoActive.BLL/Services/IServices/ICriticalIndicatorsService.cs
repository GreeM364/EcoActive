using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface ICriticalIndicatorsService
    {
        public Task<CriticalIndicatorsDTO> GetByIdAsync(string id);
        public Task<List<CriticalIndicatorsDTO>> GetAsync();
        public Task CreateAsync(CriticalIndicatorsCreateDTO request);
        public Task<List<CriticalIndicatorsDTO>> GetCriticalIndicatorsByFactoryAsync(string id);
    }
}
