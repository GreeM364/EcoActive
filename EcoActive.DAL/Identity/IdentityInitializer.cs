using Microsoft.AspNetCore.Identity;
using EcoActive.Utility;

namespace EcoActive.DAL.Identity
{
    public class IdentityInitializer
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentityInitializer(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializeRolesAsync()
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole { Name = CustomRoles.GlobalAdmin },
                new ApplicationRole { Name = CustomRoles.User },
                new ApplicationRole { Name = CustomRoles.FactoryAdmin },
                new ApplicationRole { Name = CustomRoles.Activist },
                new ApplicationRole { Name = CustomRoles.Employee }
            };

            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role.Name))
                    continue;

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
