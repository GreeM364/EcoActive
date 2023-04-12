using Microsoft.AspNetCore.Identity;

namespace EcoActive.DAL.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<ApplicationUserRole> UserRoles { get; set; }
    }
}