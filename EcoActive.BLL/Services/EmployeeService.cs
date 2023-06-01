using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Repository.IRepository;
using EcoActive.BLL.Exceptions;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFactoryRepository _factoryRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IFactoryRepository factoryRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _factoryRepository = factoryRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> GetByIdAsync(string id)
        {
            var source = await _employeeRepository.GetAsync(x => x.Id == id, "User");

            if (source == null)
            {
                throw new NotFoundException($"Employee with id {id} not found");
            }

            return _mapper.Map<EmployeeDTO>(source);
        }

        public async Task<List<EmployeeDTO>> GetAsync()
        {
            var source = await _employeeRepository.GetAllAsync(includeProperties: "User");
            return _mapper.Map<List<EmployeeDTO>>(source);
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO request)
        {
            if (await _factoryRepository.GetAsync(x => x.Id == request.FactoryId) == null)
                throw new BadRequestException($"Factory with id {request.FactoryId} doesn't exist");

            var existing = await _employeeRepository.GetAsync(x => x.User.FirstName == request.FirstName
                                                                  && x.User.LastName == request.LastName
                                                                  && x.User.Phone == request.Phone);

            if (existing != null)
                throw new BadRequestException("Employee with such parameters already exists");

            var createEntity = _mapper.Map<Employee>(request);
            await _employeeRepository.CreateAsync(createEntity, request.Password);

            var result = _mapper.Map<EmployeeDTO>(createEntity);
            return result;
        }

        public async Task<EmployeeDTO> UpdateAsync(string id, EmployeeUpdateDTO request)
        {
            var updateEntity = await _employeeRepository.GetAsync(a => a.Id == id, "User");

            if (request == null)
                throw new BadRequestException("The received model of Employee is null");

            if (updateEntity == null)
                throw new NotFoundException($"Employee with id {id} not found");


            _mapper.Map(request, updateEntity);
            await _employeeRepository.UpdateAsync(updateEntity);

            var result = _mapper.Map<EmployeeDTO>(updateEntity);
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var employee = await _employeeRepository.GetAsync(x => x.Id == id);

            if (employee == null)
                throw new NotFoundException($"Employee with such id {id} not found for deletion");

            await _employeeRepository.RemoveAsync(employee);
        }
    }
}
