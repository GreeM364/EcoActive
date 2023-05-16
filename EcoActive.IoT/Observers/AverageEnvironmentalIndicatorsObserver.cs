using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.BLL.Services.IServices;
using EcoActive.IoT.Models;
using EcoActive.IoT.Observers.IObservers;
using MQTTnet;
using Newtonsoft.Json;
using System.Text;

namespace EcoActive.IoT.Observers
{
    public class AverageEnvironmentalIndicatorsObserver : IAverageEnvironmentalIndicatorsObserver
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public AverageEnvironmentalIndicatorsObserver(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic2")
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var _environmentalIndicatorsService = scope.ServiceProvider.GetRequiredService<IEnvironmentalIndicatorsService>();

                var payload = Encoding.UTF8.GetString(message.Payload);
                var physicalCondition = JsonConvert.DeserializeObject<AverageEnvironmentalIndicators>(payload);

                var physicalConditionDTO = _mapper.Map<EnvironmentalIndicatorsCreateDTO>(physicalCondition);
                await _environmentalIndicatorsService.CreateAsync(physicalConditionDTO);
            }
        }
    }
}
