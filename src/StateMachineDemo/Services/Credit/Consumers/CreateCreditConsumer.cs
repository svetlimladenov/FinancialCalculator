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

            // Here you we usually build a routing slip, with a list of activities
            // the routing slip was already showed by Steliyan's Demo (ST6)
            // https://github.com/steliyan/Demo-MassTransit-ST6 

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

            // When using Routing Slip we don't manually send the response, but we use 
            // routing slip subscribers, which basicly wait for the Routing Slip to complete
            // and if it completed succesfully we specify what message to be send
            // and same when it faults
        }
    }
}
