using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Credit
{
    public class Program
    {
        public static async Task Main()
        {
            var hostBuilder = new HostBuilder();

            hostBuilder
                .ConfigureServices((hostBuilder, serviceCollection) =>
                {
                    serviceCollection.AddHostedService<CreditService>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
