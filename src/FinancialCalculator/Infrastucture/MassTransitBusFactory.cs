using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace Infrastucture
{
    public static class MassTransitBusFactory
    {
        public static void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.Host(new Uri("rabbitmq://rabbit/test"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            configurator.ConfigureEndpoints(context);
        }
    }
}
