﻿using EcoActive.DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EcoActive.BLL.Services.IServices;
using EcoActive.BLL.Services;
using EcoActive.BLL.Mappings;
using EcoActive.BLL.BrainTree;

namespace EcoActive.BLL.Infrastructure
{
    public static class BusinessLogicLayerExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessLayer(configuration);
            services.AddAutoMapper(typeof(ActivistProfile));
            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddAutoMapper(typeof(FactoryProfile));
            services.AddAutoMapper(typeof(FactoryAdministratorProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(CriticalIndicatorsProfile));
            services.AddAutoMapper(typeof(EnvironmentalIndicatorsProfile));

            services.AddScoped<IActivistService, ActivistService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFactoryService, FactoryService>();
            services.AddScoped<IFactoryAdministratorService, FactoryAdministratorService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICriticalIndicatorsService, CriticalIndicatorsService>();
            services.AddScoped<IEnvironmentalIndicatorsService, EnvironmentalIndicatorsService>();

            services.Configure<BrainTreeSettings>(configuration.GetSection("BrainTree"));
            services.AddSingleton<IBrainTreeGate, BrainTreeGate>();

            return services;
        }
    }
}
