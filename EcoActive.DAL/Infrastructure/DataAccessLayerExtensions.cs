using EcoActive.DAL.Data;
using EcoActive.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcoActive.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EcoActiveDbContext>(options => options.UseSqlServer(connectionString));


            services.AddIdentity<ApplicationUser, ApplicationRole>()
                   .AddEntityFrameworkStores<EcoActiveDbContext>()
                   .AddDefaultTokenProviders();

            services.AddTransient<IdentityInitializer>();
            services.BuildServiceProvider().GetService<IdentityInitializer>().InitializeRolesAsync();

            services.AddScoped<RoleManager<ApplicationRole>>();


            return services;
        }
    }
}
