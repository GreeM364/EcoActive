using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.BLL.Services
{
    public class EnvironmentalIndicatorsService : IEnvironmentalIndicatorsService
    {

        private readonly IEnvironmentalIndicatorsRepository _environmentalIndicatorsRepository;
        private readonly IFactoryRepository _factoryRepository;
        private readonly IMapper _mapper;

        public EnvironmentalIndicatorsService(IEnvironmentalIndicatorsRepository environmentalIndicatorsRepository,
                                              IFactoryRepository factoryRepository, IMapper mapper)
        {
            _environmentalIndicatorsRepository = environmentalIndicatorsRepository;
            _factoryRepository = factoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(EnvironmentalIndicatorsCreateDTO request)
        {
            if (await _factoryRepository.GetAsync(p => p.Id == request.FactoryId) == null)
                throw new NotFoundException($"Factory with Id {request.FactoryId} does exist");
            if (request == null)
                throw new BadRequestException("The request model of EnvironmentalIndicators is null");

            var createEntity = _mapper.Map<EnvironmentalIndicators>(request);
            await _environmentalIndicatorsRepository.CreateAsync(createEntity);
        }

        public async Task<List<EnvironmentalIndicatorsDTO>> GetAsync()
        {
            var source = await _environmentalIndicatorsRepository.GetAllAsync();
            return _mapper.Map<List<EnvironmentalIndicatorsDTO>>(source);
        }

        public async Task<EnvironmentalIndicatorsDTO> GetByIdAsync(string id)
        {
            var source = await _environmentalIndicatorsRepository.GetByIdAsync(id);

            if (source == null)
            {
                throw new NotFoundException($"Environmental Indicators with id {id} not found");
            }

            return _mapper.Map<EnvironmentalIndicatorsDTO>(source);
        }

        public async Task<List<EnvironmentalIndicatorsDTO>> GetEnvironmentalIndicatorsByFactoryAsync(string id)
        {
            if (await _factoryRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Factory with id {id} not found");

            var source = await _environmentalIndicatorsRepository.GetAllAsync(x => x.FactoryId == id);

            var environmentalIndicators = _mapper.Map<List<EnvironmentalIndicatorsDTO>>(source);
            return environmentalIndicators;
        }
    }
}
