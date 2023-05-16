using MQTTnet;

namespace EcoActive.IoT.Observers.IObservers
{
    public interface IMqttMessageObserver
    {
        Task HandleMessageAsync(MqttApplicationMessage message);
    }
}
