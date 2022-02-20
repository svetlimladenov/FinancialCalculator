using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class CreateCreditConsumer : IConsumer<CreateCreditRequested>
    {
        private readonly ILogger<CreateCreditConsumer> logger;

        public CreateCreditConsumer(ILogger<CreateCreditConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateCreditRequested> context)
        {
            var createCredit = context.Message.CreateCredit;
            this.logger.LogDebug("Creating credit with id {0}, amount - {1}", createCredit.CreditId, createCredit.Amount);

            await Task.Delay(2000);

            if (createCredit.Period < 0)
            {
                await context.RespondAsync<CreateCreditFaulted>(new
                {
                    createCredit.CreditId,
                    ValidationErrors = new List<int>() { 15 }
                });
            }
            else
            {
                await context.RespondAsync<CreateCreditCompleted>(new
                {
                    createCredit.CreditId
                });
            }
        }
    }
}
