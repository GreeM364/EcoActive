using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using EcoActive.IoT.Hubs;
using EcoActive.IoT.Observers.IObservers;
using MQTTnet;
using Newtonsoft.Json;
using System.Text;
using EcoActive.IoT.Models;
using EcoActive.BLL.Services.IServices;
using EcoActive.BLL.DataTransferObjects;


namespace EcoActive.IoT.Observers
{
    public class CriticalIndicatorsObserver : ICriticalIndicatorsObserver
    {
        private readonly IHubContext<EnvironmentalIndicatorHub> _hub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public CriticalIndicatorsObserver(IHubContext<EnvironmentalIndicatorHub> hub, IServiceScopeFactory serviceScopeFactory,
                                         IMapper mapper)
        {
            _hub = hub;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic3")
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var payload = Encoding.UTF8.GetString(message.Payload);
                var criticalEnvironmental = JsonConvert.DeserializeObject<CriticalEnvironmentalIndicators>(payload)!;

                await Send(criticalEnvironmental);
                await Save(scope, criticalEnvironmental);
            }
        }

        private async Task Save(IServiceScope scope, CriticalEnvironmentalIndicators criticalEnvironmental)
        {
            var criticalIndicatorsService = scope.ServiceProvider.GetRequiredService<ICriticalIndicatorsService>();
            var criticalIndicatorsDTO = _mapper.Map<CriticalIndicatorsCreateDTO>(criticalEnvironmental);

            await criticalIndicatorsService.CreateAsync(criticalIndicatorsDTO);
        }

        private async Task Send(CriticalEnvironmentalIndicators criticalEnvironmental)
        {
            await _hub.Clients.Group($"{criticalEnvironmental.FactoryId}").SendAsync("CriticalIndicators", criticalEnvironmental);
        }
    }
}
