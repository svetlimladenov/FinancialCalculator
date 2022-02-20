using Contracts;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payment.Consumers
{
    public class AddBonusPointsConsumer : IConsumer<AddBonusPointsRequested>
    {
        public async Task Consume(ConsumeContext<AddBonusPointsRequested> context)
        {
            var bonusPoints = context.Message.BonusPoints;
            if (bonusPoints.Amount < 0)
            {
                await context.RespondAsync<AddBonusPointsFaulted>(new
                {
                    bonusPoints.CreditId,
                    ValidationErrors = new List<int>() { 99 }
                });
            }
            else
            {
                await context.RespondAsync<AddBonusPointsCompleted>(new
                {
                    bonusPoints.CreditId
                });
            }
        }
    }
}
