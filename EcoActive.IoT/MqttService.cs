﻿using EcoActive.IoT.Models;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using MQTTnet;
using EcoActive.IoT.Observers;
using EcoActive.IoT.Observers.IObservers;

namespace EcoActive.IoT
{
    public class MqttService : BackgroundService
    {
        private readonly MqttSeting _mqttSeting;
        private IManagedMqttClient _mqttClient;
        private readonly MqttMessageHandler _messageHandler;

        public MqttService(IConfiguration configuration, IRealTimeEnvironmentalIndicatorsObserver realTimeIndicatorsObserver,
                           IAverageEnvironmentalIndicatorsObserver averageIndicatorsObserver,
                           ICriticalIndicatorsObserver criticalIndicatorsObserver)
        {
            _messageHandler = new MqttMessageHandler();
            _mqttSeting = configuration.GetSection("Mqtt").Get<MqttSeting>()!;
            _messageHandler.RegisterObserver(realTimeIndicatorsObserver);
            _messageHandler.RegisterObserver(averageIndicatorsObserver);
            _messageHandler.RegisterObserver(criticalIndicatorsObserver);
        }

        public async Task ConnectAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId(_mqttSeting.ClientId)
                .WithTcpServer(_mqttSeting.BrokerAddress)
                .WithCredentials(_mqttSeting.Username, _mqttSeting.Password)
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();

            _mqttClient = new MqttFactory().CreateManagedMqttClient();
            _mqttClient.ApplicationMessageReceivedAsync += e => Task.Run(() => HandleIncomingMessage(e.ApplicationMessage));
            await _mqttClient.SubscribeAsync("test/topic1", MqttQualityOfServiceLevel.AtLeastOnce);
            await _mqttClient.SubscribeAsync("test/topic2", MqttQualityOfServiceLevel.AtLeastOnce);
            await _mqttClient.SubscribeAsync("test/topic3", MqttQualityOfServiceLevel.AtLeastOnce);
            await _mqttClient.StartAsync(managedOptions);
        }


        private async void HandleIncomingMessage(MqttApplicationMessage message)
        {
            await _messageHandler.HandleMessageAsync(message);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            await _mqttClient.StopAsync();
        }
    }
}
