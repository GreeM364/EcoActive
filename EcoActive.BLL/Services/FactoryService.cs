using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.BLL.Services
{
    public class FactoryService : IFactoryService
    {
        private readonly IFactoryRepository _factoryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public FactoryService(IFactoryRepository factoryRepository, IEmployeeRepository employeeRepository,
                              IMapper mapper)
        {
            _factoryRepository = factoryRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<FactoryDTO> GetByIdAsync(string id)
        {
            var source = await _factoryRepository.GetByIdAsync(id);

            if (source == null)
            {
                throw new NotFoundException($"Factory with id {id} not found");
            }

            return _mapper.Map<FactoryDTO>(source);
        }

        public async Task<List<FactoryDTO>> GetAsync()
        {
            var source = await _factoryRepository.GetAllAsync();
            return _mapper.Map<List<FactoryDTO>>(source);
        }

        public async Task<FactoryDTO> CreateAsync(FactoryCreateDTO request)
        {
            var existing = await _factoryRepository.GetAsync(x => x.Name == request.Name);

            if (existing != null)
            {
                throw new BadRequestException($"Factory with name {request.Name} already exists");
            }
            if (request == null)
            {
                throw new BadRequestException("The request model of Factory is null");
            }

            var createEntity = _mapper.Map<Factory>(request);
            await _factoryRepository.CreateAsync(createEntity);

            var result = _mapper.Map<FactoryDTO>(createEntity);
            return result;
        }

        public async Task<FactoryDTO> UpdateAsync(string id, FactoryUpdateDTO request)
        {
            var updateEntity = await _factoryRepository.GetByIdAsync(id);

            if (request == null)
                throw new BadRequestException("The received model of Factory is null");

            if (updateEntity == null)
                throw new NotFoundException($"Factory with id {id} not found");

            if (await _factoryRepository.GetAsync(x => x.Name == request.Name && x.Id != id) != null)
                throw new BadRequestException("Factory with such name already exists");

            if (await _factoryRepository.GetAsync(x => x.Territory == request.Territory && x.Id != id) != null)
                throw new BadRequestException("Factory with such location already exists");

            _mapper.Map(request, updateEntity);
            await _factoryRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<FactoryDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var factory = await _factoryRepository.GetAsync(x => x.Id == id);

            if (factory == null)
                throw new NotFoundException($"Factory with such id {id} not found for deletion");

            await _factoryRepository.RemoveAsync(factory);
        }

        public async Task<List<EmployeeDTO>> GetEmployeesAsync(string id)
        {
            if (await _factoryRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Factory with such id {id} not found");

            var source = await _employeeRepository.GetAllAsync(x => x.FactoryId == id, includeProperties: "User",
                                                               isTracking: false);

            var employee = _mapper.Map<List<EmployeeDTO>>(source);
            return employee;
        }
    }
}
