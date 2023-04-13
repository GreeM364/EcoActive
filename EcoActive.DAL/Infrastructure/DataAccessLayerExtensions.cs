using EcoActive.DAL.Data;
using EcoActive.DAL.Identity;
using EcoActive.DAL.Repository.IRepository;
using EcoActive.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcoActive.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EcoActiveDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IActivistRepository, ActivistRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFactoryAdminRepository, FactoryAdminRepository>();
            services.AddScoped<IFactoryRepository, FactoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                   .AddEntityFrameworkStores<EcoActiveDbContext>()
                   .AddDefaultTokenProviders();

            services.AddTransient<IdentityInitializer>();
            services.BuildServiceProvider().GetService<IdentityInitializer>().InitializeRolesAsync();

            services.AddScoped<RoleManager<ApplicationRole>>();

            services.AddSingleton<IConfiguration>(configuration);


            services.AddScoped<JwtHandler>();
            var key = configuration.GetValue<string>("JwtSettings:Secret");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
