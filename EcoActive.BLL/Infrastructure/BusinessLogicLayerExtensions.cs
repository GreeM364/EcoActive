using EcoActive.DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EcoActive.BLL.Infrastructure
{
    public static class BusinessLogicLayerExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessLayer(connectionString);

            return services;
        }
    }
}
