using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class DeleteCreditConsumer : IConsumer<DeleteCreditRequested>
    {
        private readonly ILogger<DeleteCreditConsumer> logger;

        public DeleteCreditConsumer(ILogger<DeleteCreditConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<DeleteCreditRequested> context)
        {
            logger.LogDebug("Deleting credit with id {0}", context.Message.CreditId);

            await Task.Delay(2000);

            await context.RespondAsync<DeleteCreditCompleted>(new
            {
                context.Message.CreditId
            });
        }
    }
}
