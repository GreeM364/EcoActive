using EcoActive.DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EcoActive.BLL.Services.IServices;
using EcoActive.BLL.Services;
using EcoActive.BLL.Mappings;

namespace EcoActive.BLL.Infrastructure
{
    public static class BusinessLogicLayerExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessLayer(configuration);
            services.AddAutoMapper(typeof(EmployeeProfile));

            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
