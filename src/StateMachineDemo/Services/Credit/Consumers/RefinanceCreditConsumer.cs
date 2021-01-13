using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class RefinanceCreditConsumer : IConsumer<RefinanceCreditRequested>
    {
        private readonly ILogger<RefinanceCreditConsumer> logger;

        public RefinanceCreditConsumer(ILogger<RefinanceCreditConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<RefinanceCreditRequested> context)
        {
            var refinanceCredit = context.Message.RefinanceCredit;

            logger.LogDebug("Refinancing credit with id {0}, with the new credit with id {1}", refinanceCredit.RefinancedCreditId, refinanceCredit.ParentCreditId);
            await Task.Delay(2000);

            if (refinanceCredit.RefinancePaymentAmount < 0)
            {
                await context.RespondAsync<RefinanceCreditFaulted>(new
                {
                    refinanceCredit.ParentCreditId,
                    ValidationErrors = new List<int>() { 1500 }
                });
            }
            else
            {
                await context.RespondAsync<RefinanceCreditCompleted>(new
                {
                    refinanceCredit.ParentCreditId,
                    ValidationErrors = new List<int>() { 1500 }
                });
            }
        }
    }
}
