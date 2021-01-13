using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace Payment
{
    public class Program
    {
        public static async Task Main()
        {
            var hostBuilder = new HostBuilder();

            hostBuilder
                .ConfigureServices((hostBuilder, serviceCollection) =>
                {
                    serviceCollection.AddHostedService<PaymentService>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
