using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Services.IServices;
using EcoActive.DAL.Identity;
using EcoActive.DAL.Infrastructure;
using EcoActive.DAL.Repository.IRepository;
using EcoActive.Utility;
using Microsoft.AspNetCore.Identity;

namespace EcoActive.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginService(UserManager<ApplicationUser> userManager, IEmployeeRepository employeeRepository, 
                            JwtHandler jwtHandler)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }
        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new LoginResultDTO()
                {
                    ErrorMessage = "Invalid Authentication"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                if (role == CustomRoles.Employee)
                {
                    var employee = await _employeeRepository.GetAsync(d => d.UserId == user.Id, "Factory");

                    bool checkSubscription = CheckSubscription(employee.Factory.DataPaySubscription);

                    if (!checkSubscription)
                        return new LoginResultDTO() { ErrorMessage = "Subscription expired" };

                    break;
                }
            }

            string token = await _jwtHandler.GenerateJwtToken(user);
            return new LoginResultDTO() { IsSuccess = true, Token = token };
        }

        private bool CheckSubscription(DateTime dataPaySubscription)
        {
            var check = (DateTime.Today - dataPaySubscription).TotalDays - 30;

            if (check > 0)
                return false;

            return true;
        }
    }
}
