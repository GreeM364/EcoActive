using AutoMapper;
using Braintree;
using EcoActive.BLL.BrainTree;
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
        private readonly IFactoryAdminRepository _factoryAdminRepository;
        private readonly IActivistRepository _activistRepository;
        public readonly IBrainTreeGate _brainTreeGate;
        private readonly IMapper _mapper;

        public FactoryService(IFactoryRepository factoryRepository, IEmployeeRepository employeeRepository,
                             IFactoryAdminRepository factoryAdminRepository, IActivistRepository activistRepository,
                             IBrainTreeGate brainTreeGate, IMapper mapper)
        {
            _factoryRepository = factoryRepository;
            _employeeRepository = employeeRepository;
            _factoryAdminRepository = factoryAdminRepository;
            _activistRepository = activistRepository;
            _brainTreeGate = brainTreeGate;
            _mapper = mapper;
        }

        public async Task<FactoryDTO> GetByIdAsync(string id)
        {
            var source = await _factoryRepository.GetByIdAsync(id);
            var employeesCount = await _employeeRepository.GetAllAsync(e => e.FactoryId == id);
            var factoryAdminsCount = await _factoryAdminRepository.GetAllAsync(a => a.FactoryId == id);

            if (source == null)
            {
                throw new NotFoundException($"Factory with id {id} not found");
            }

            var factoryDTO = _mapper.Map<FactoryDTO>(source);
            factoryDTO.EmployeesCount = employeesCount.Count();
            factoryDTO.FactoryAdminsCount = factoryAdminsCount.Count();

            return factoryDTO;
        }

        public async Task<List<FactoryDTO>> GetAsync()
        {
            var source = await _factoryRepository.GetAllAsync();
            var factoriesDTO = _mapper.Map<List<FactoryDTO>>(source);

            factoriesDTO = factoriesDTO.Select(async f =>
            {
                var employeesCount = (await _employeeRepository.GetAllAsync(e => e.FactoryId == f.Id)).Count();
                var factoryAdminsCount = (await _factoryAdminRepository.GetAllAsync(a => a.FactoryId == f.Id)).Count();
                return new FactoryDTO
                {
                    Id = f.Id,
                    Name = f.Name,
                    Type = f.Type,
                    Territory = f.Territory,
                    ActivistId = f.ActivistId,
                    DataPaySubscription = f.DataPaySubscription,
                    EmployeesCount = employeesCount,
                    FactoryAdminsCount = factoryAdminsCount
                };
            }).Select(t => t.Result).ToList();

            return factoriesDTO;
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

        public async Task<ClientTokenDTO> GetToken()
        {
            var gateway = _brainTreeGate.GetGateway();
            var clientToken = await gateway.ClientToken.GenerateAsync();

            return new ClientTokenDTO { ClientToken = clientToken };
        }

        public async Task PaySubscription(string id, PaymentNonceDTO nonceDTO)
        {
            var factory = await _factoryRepository.GetAsync(x => x.Id == id);

            if (factory == null)
                throw new NotFoundException($"Factory with such id {id} not found");

            var request = new TransactionRequest
            {
                Amount = 1000,
                PaymentMethodNonce = nonceDTO.PaymentNonce,
                OrderId = Guid.NewGuid().ToString(),
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var gateway = _brainTreeGate.GetGateway();
            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                factory.DataPaySubscription = DateTime.Today;
                await _factoryRepository.UpdateAsync(factory);
            }
            else
            {
                throw new BadRequestException($"An error occurred while paying for the subscription");
            }
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

        public async Task<List<FactoryAdministratorDTO>> GetFactoryAdministratorsAsync(string id)
        {
            if (await _factoryRepository.GetAsync(x => x.Id == id) == null)
                throw new NotFoundException($"Factory with such id {id} not found");

            var source = await _factoryAdminRepository.GetAllAsync(x => x.FactoryId == id, includeProperties: "User",
                                                                   isTracking: false);

            var employee = _mapper.Map<List<FactoryAdministratorDTO>>(source);
            return employee;
        }

        public async Task<ActivistDTO> GetActivistAsync(string id)
        {
            var factory = await _factoryRepository.GetAsync(x => x.Id == id);
            if (factory == null)
                throw new NotFoundException($"Factory with id {id} not found");

            var source = await _activistRepository.GetAsync(x => x.Id == factory.ActivistId,
                                                            includeProperties: "User", isTracking: false);
            if (source == null)
                return new ActivistDTO();

            var result = _mapper.Map<ActivistDTO>(source);
            return result;
        }
    }
}
