using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Credit
{
    public class CreditService : IHostedService
    {
        private readonly IBusControl busControl;

        public CreditService(IBusControl busControl)
        {
            this.busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await busControl.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await busControl.StopAsync();
        }
    }
}
