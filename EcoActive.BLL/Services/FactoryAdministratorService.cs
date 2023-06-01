using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.BLL.Services
{
    public class FactoryAdministratorService : IFactoryAdministratorService
    {
        private readonly IFactoryAdminRepository _factoryAdministratorRepository;
        private readonly IFactoryRepository _factoryRepository;
        private readonly IMapper _mapper;

        public FactoryAdministratorService(IFactoryAdminRepository factoryAdministratorRepository,
                                           IFactoryRepository factoryRepository,IMapper mapper)
        {
            _factoryAdministratorRepository = factoryAdministratorRepository;
            _factoryRepository = factoryRepository;
            _mapper = mapper;
        }

        public async Task<FactoryAdministratorDTO> GetByIdAsync(string id)
        {
            var source = await _factoryAdministratorRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new NotFoundException($"Factory Administrator with id {id} not found");
            }

            return _mapper.Map<FactoryAdministratorDTO>(source);
        }

        public async Task<List<FactoryAdministratorDTO>> GetAsync()
        {
            var source = await _factoryAdministratorRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<FactoryAdministratorDTO>>(source);
        }

        public async Task<FactoryAdministratorDTO> CreateAsync(FactoryAdministratorCreateDTO request)
        {
            if (await _factoryRepository.GetAsync(x => x.Id == request.FactoryId) == null)
                throw new BadRequestException($"Factory with id {request.FactoryId} doesn't exist");

            var existing = await _factoryAdministratorRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                                && x.User.LastName == request.LastName
                                                                                && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("Factory Administrator with such parameters already exists");

            var createEntity = _mapper.Map<FactoryAdmin>(request);
            await _factoryAdministratorRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<FactoryAdministratorDTO>(createEntity);
            return result;
        }

        public async Task<FactoryAdministratorDTO> UpdateAsync(string id, FactoryAdministratorUpdateDTO request)
        {
            var updateEntity = await _factoryAdministratorRepository.GetAsync(a => a.Id == id, "User");

            if (request == null)
                throw new BadRequestException("The received model of Factory Administrator is null");

            if (updateEntity == null)
                throw new NotFoundException($"Factory Administrator with id {id} not found");


            _mapper.Map(request, updateEntity);
            await _factoryAdministratorRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<FactoryAdministratorDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var factoryAdministrator = await _factoryAdministratorRepository.GetAsync(x => x.Id == id);

            if (factoryAdministrator == null)
                throw new NotFoundException($"Factory Administrator with such id {id} not found for deletion");

            await _factoryAdministratorRepository.RemoveAsync(factoryAdministrator);
        }
    }
}
