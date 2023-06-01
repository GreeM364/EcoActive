using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.BLL.Services
{
    public class ActivistService : IActivistService
    {
        private readonly IActivistRepository _activistRepository;
        private readonly IFactoryRepository _factoryRepository;
        private readonly IMapper _mapper;

        public ActivistService(IActivistRepository activistRepository, IFactoryRepository factoryRepository,
                               IMapper mapper)
        {
            _activistRepository = activistRepository;
            _factoryRepository = factoryRepository;
            _mapper = mapper;
        }

        public async Task<ActivistDTO> GetByIdAsync(string id)
        {
            var source = await _activistRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new NotFoundException($"Activist with id {id} not found");
            }

            return _mapper.Map<ActivistDTO>(source);
        }

        public async Task<List<ActivistDTO>> GetAsync()
        {
            var source = await _activistRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<ActivistDTO>>(source);
        }

        public async Task<ActivistDTO> CreateAsync(ActivistCreateDTO request)
        {
            var existing = await _activistRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                                && x.User.LastName == request.LastName
                                                                                && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("Activist with such parameters already exists");

            var createEntity = _mapper.Map<Activist>(request);
            await _activistRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<ActivistDTO>(createEntity);
            return result;
        }

        public async Task<ActivistDTO> UpdateAsync(string id, ActivistUpdateDTO request)
        {
            var updateEntity = await _activistRepository.GetAsync(a => a.Id == id, "User"); 

            if (request == null)
                throw new BadRequestException("The received model of Activist is null");

            if (updateEntity == null)
                throw new NotFoundException($"Activist with id {id} not found");


            _mapper.Map(request, updateEntity);
            await _activistRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<ActivistDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var activist = await _activistRepository.GetAsync(x => x.Id == id);

            if (activist == null)
                throw new NotFoundException($"Activist with such id {id} not found for deletion");

            await _activistRepository.RemoveAsync(activist);
        }

        public async Task<List<FactoryDTO>> GetFactoriesAsync(string id)
        {
            if (await _activistRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Activist with such id {id} not found");

            var source = await _factoryRepository.GetAllAsync(x => x.ActivistId == id);

            var result = _mapper.Map<List<FactoryDTO>>(source);
            return result;
        }

        public async Task AddFactoryToActivistAsync(string activistId, AddFactoryToActivistDTO request)
        {
            var factory = await _factoryRepository.GetByIdAsync(request.FactoryId);

            if (factory == null)
                throw new NotFoundException($"Factory with id {request.FactoryId} not found");

            if (await _activistRepository.GetAsync(x => x.Id == activistId) == null)
                throw new NotFoundException($"Activist with id {activistId} not found");

            factory.ActivistId = activistId;
            await _factoryRepository.UpdateAsync(factory);
        }

        public async Task DeleteFactoryToActivistAsync(string factoryId)
        {
            var factory = await _factoryRepository.GetByIdAsync(factoryId);

            if (factory == null)
                throw new NotFoundException($"Factory with id {factoryId} not found");

            factory.ActivistId = null;
            await _factoryRepository.UpdateAsync(factory);
        }
    }
}
