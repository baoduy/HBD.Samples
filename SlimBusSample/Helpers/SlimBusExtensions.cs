using Azure.Messaging.ServiceBus;
using SlimBusSample.Models;
using SlimMessageBus.Host.AzureServiceBus;
using SlimMessageBus.Host.Config;

namespace SlimBusSample.Helpers;

public static class SlimBusExtensions
{
    public static ProducerBuilder<T> WithBusMessageModifier<T>(this ProducerBuilder<T> producerBuilder) =>
        producerBuilder.WithModifier((message, busMessage) =>
        {
            var type = message!.GetType();
            
            if (message is not IBusMessage msg) return;
            
            busMessage.SessionId = msg.SessionId;
            busMessage.ReplyTo = msg.ReplyTo;
            busMessage.ReplyToSessionId = msg.ReplyToSessionId;

            if (type.GetCustomAttributes(typeof(BusMessagePropertiesAttribute), true).FirstOrDefault() is not BusMessagePropertiesAttribute att) return;
            foreach (var p in att.Properties)
            {
                var value = type.GetProperty(p)!.GetValue(message);
                if (value is null || value is string v && string.IsNullOrWhiteSpace(v)) continue;

                busMessage.ApplicationProperties.Add(p, value);
            }
        });
}