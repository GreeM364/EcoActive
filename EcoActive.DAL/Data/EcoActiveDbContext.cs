using EcoActive.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoActive.DAL.Data
{
    public class EcoActiveDbContext : DbContext
    {
        public EcoActiveDbContext(DbContextOptions<EcoActiveDbContext> options) : base(options) { }

        DbSet<Factory> Factories { get; set; }
        DbSet<FactoryAdmin> FactoryAdmins { get; set; }
        DbSet<FactoryEmployee> FactoryEmployees { get; set; }
        DbSet<Activist> Activists { get; set;}

    }
}
