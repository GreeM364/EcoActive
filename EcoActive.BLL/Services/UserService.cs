using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Exceptions;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Repository.IRepository;

namespace EcoActive.BLL.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ProfileDTO> GetProfileAsync(string userId)
        {
            var source = await _userRepository.GetByIdAsync(userId);

            if (source == null)
                throw new NotFoundException($"User with id {userId} not found");

            var result = _mapper.Map<ProfileDTO>(source);
            return result;
        }
    }
}
