using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Payment
{
    public class PaymentService : IHostedService
    {
        private readonly IBusControl busControl;

        public PaymentService(IBusControl busControl)
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
