using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.BLL.Services.IServices
{
    public interface IEnvironmentalIndicatorsService
    {
        public Task<EnvironmentalIndicatorsDTO> GetByIdAsync(string id);
        public Task<List<EnvironmentalIndicatorsDTO>> GetAsync();
        public Task CreateAsync(EnvironmentalIndicatorsCreateDTO request);
        public Task<List<EnvironmentalIndicatorsDTO>> GetEnvironmentalIndicatorsByFactoryAsync(string id);
    }
}
