using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class CalculateCreditConsumer : IConsumer<CalculateCreditRequested>
    {
        private readonly ILogger<CalculateCreditConsumer> logger;

        public CalculateCreditConsumer(ILogger<CalculateCreditConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<CalculateCreditRequested> context)
        {
            this.logger.LogDebug("Calculating credit...");

            await context.RespondAsync<CalculateCreditResponse>(new
            {
                InterestSum = 12
            });
        }
    }
}
