﻿using Microsoft.AspNetCore.SignalR;
using EcoActive.BLL.Services.IServices;
using EcoActive.IoT.Models;
using EcoActive.IoT.Observers.IObservers;
using MQTTnet;
using Newtonsoft.Json;
using System.Text;
using EcoActive.IoT.Hubs;

namespace EcoActive.IoT.Observers
{
    public class RealTimeEnvironmentalIndicatorsObserver : IRealTimeEnvironmentalIndicatorsObserver
    {
        private readonly IHubContext<EnvironmentalIndicatorHub> _hub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RealTimeEnvironmentalIndicatorsObserver(IHubContext<EnvironmentalIndicatorHub> hub, IServiceScopeFactory serviceScopeFactory)
        {
            _hub = hub;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic1")
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var factoryService = scope.ServiceProvider.GetRequiredService<IFactoryService>();

                var payload = Encoding.UTF8.GetString(message.Payload);
                var realTimeEnvironmentalIndicators = JsonConvert.DeserializeObject<RealTimeEnvironmentalIndicators>(payload)!;

                var activist = await factoryService.GetActivistAsync(realTimeEnvironmentalIndicators.FactoryId);
                var employees = await factoryService.GetEmployeesAsync(realTimeEnvironmentalIndicators.FactoryId);

                await _hub.Clients.All.SendAsync("EnvironmentalIndicators", realTimeEnvironmentalIndicators);
            }
        }
    }
}
