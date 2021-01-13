using Credit.StateMachines;
using Infrastucture;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace Credit
{
    public class Program
    {
        public static async Task Main()
        {
            LoggerFactory.SetupMassTransitLogger();

            var hostBuilder = new HostBuilder();

            hostBuilder
                .ConfigureServices((hostBuilder, serviceCollection) =>
                {
                    serviceCollection.AddMassTransit(cfg =>
                    {
                        var thisAssembly = typeof(CreditService).Assembly;
                        cfg.UsingRabbitMq(MassTransitBusFactory.ConfigureBus);

                        cfg.AddConsumers(thisAssembly);
                        cfg.AddSagaStateMachine<UtilizeCreditStateMachine, UtilizeCredit>().InMemoryRepository();
                    });

                    serviceCollection.AddHostedService<CreditService>();
                })
                .ConfigureLogging((hostingContext, logging) => logging.AddSerilog(dispose: true));

            await hostBuilder.RunConsoleAsync();
        }
    }
}
