using Microsoft.AspNetCore.SignalR;
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
        public RealTimeEnvironmentalIndicatorsObserver(IHubContext<EnvironmentalIndicatorHub> hub)
        {
            _hub = hub;
        }

        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic1")
            {
                var payload = Encoding.UTF8.GetString(message.Payload);
                var realTimeEnvironmentalIndicators = JsonConvert.DeserializeObject<RealTimeEnvironmentalIndicators>(payload)!;

                await _hub.Clients.Group($"{realTimeEnvironmentalIndicators.FactoryId}").SendAsync("EnvironmentalIndicators", realTimeEnvironmentalIndicators);
            }
        }
    }
}
