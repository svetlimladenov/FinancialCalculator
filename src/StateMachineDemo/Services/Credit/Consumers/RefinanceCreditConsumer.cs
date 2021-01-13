using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class RefinanceCreditConsumer : IConsumer<RefinanceCreditRequested>
    {
        public async Task Consume(ConsumeContext<RefinanceCreditRequested> context)
        {
            var refinanceCredit = context.Message.RefinanceCredit;
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
