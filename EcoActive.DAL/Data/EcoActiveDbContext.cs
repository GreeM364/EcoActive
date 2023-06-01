using EcoActive.DAL.Entities;
using EcoActive.DAL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoActive.DAL.Data
{
    public class EcoActiveDbContext : IdentityDbContext<ApplicationUser>
    {
        public EcoActiveDbContext(DbContextOptions<EcoActiveDbContext> options) : base(options) { }

        public DbSet<User> BaseUsers { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<FactoryAdmin> FactoryAdmins { get; set; }
        public DbSet<Employee> FactoryEmployees { get; set; }
        public DbSet<Activist> Activists { get; set;}
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<EnvironmentalIndicators> EnvironmentalIndicators { get; set; }
        public DbSet<CriticalIndicators> CriticalIndicators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ActivistConfiguration());
        }

    }
}
