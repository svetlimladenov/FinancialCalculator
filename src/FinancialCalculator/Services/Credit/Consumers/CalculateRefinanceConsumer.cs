using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class CalculateRefinanceCredit : IConsumer<CalculateRefinanceRequested>
    {
        private readonly ILogger<CalculateCreditConsumer> logger;

        public CalculateRefinanceCredit(ILogger<CalculateCreditConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<CalculateRefinanceRequested> context)
        {
            await context.RespondAsync<RefinanceCreditResponse>(new
            {
                CreditSum = context.Message.CreditSum - context.Message.PaymentsMade * 100
            });
        }
    }
}
