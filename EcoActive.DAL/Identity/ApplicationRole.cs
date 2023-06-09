﻿using Microsoft.AspNetCore.Identity;

namespace EcoActive.DAL.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() {}
        public ApplicationRole(string role) : base(role) { }

        public virtual List<ApplicationUserRole> UserRoles { get; set; }
    }
}
