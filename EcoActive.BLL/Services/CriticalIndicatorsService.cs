using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.BLL.Services
{
    public class CriticalIndicatorsService : ICriticalIndicatorsService
    {

        private readonly ICriticalIndicatorsRepository _criticalIndicatorsRepository;
        private readonly IFactoryRepository _factoryRepository;
        private readonly IMapper _mapper;

        public CriticalIndicatorsService(ICriticalIndicatorsRepository criticalIndicatorsRepository,
                                         IFactoryRepository factoryRepository, IMapper mapper)
        {
            _criticalIndicatorsRepository = criticalIndicatorsRepository;
            _factoryRepository = factoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CriticalIndicatorsCreateDTO request)
        {
            if (await _factoryRepository.GetAsync(p => p.Id == request.FactoryId) == null)
                throw new NotFoundException($"Factory with Id {request.FactoryId} does exist");
            if (request == null)
                throw new BadRequestException("The request model of CriticalIndicators is null");

            var createEntity = _mapper.Map<CriticalIndicators>(request);
            await _criticalIndicatorsRepository.CreateAsync(createEntity);
        }

        public async Task<List<CriticalIndicatorsDTO>> GetAsync()
        {
            var source = await _criticalIndicatorsRepository.GetAllAsync();
            return _mapper.Map<List<CriticalIndicatorsDTO>>(source);
        }

        public async Task<CriticalIndicatorsDTO> GetByIdAsync(string id)
        {
            var source = await _criticalIndicatorsRepository.GetByIdAsync(id);

            if (source == null)
            {
                throw new NotFoundException($"Critical Indicators with id {id} not found");
            }

            return _mapper.Map<CriticalIndicatorsDTO>(source);
        }

        public async Task<List<CriticalIndicatorsDTO>> GetCriticalIndicatorsByFactoryAsync(string id)
        {
            if (await _factoryRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Factory with id {id} not found");

            var source = await _criticalIndicatorsRepository.GetAllAsync(x => x.FactoryId == id);

            var criticalIndicators = _mapper.Map<List<CriticalIndicatorsDTO>>(source);
            return criticalIndicators;
        }
    }
}
