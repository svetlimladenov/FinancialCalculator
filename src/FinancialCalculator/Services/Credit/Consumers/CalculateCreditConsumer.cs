using Contracts;
using Credit.Utils;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class CalculateCreditConsumer : IConsumer<CalculateCreditRequested>
    {
        private readonly ILogger<CalculateCreditConsumer> logger;
        private readonly ICalculator calculator;

        public CalculateCreditConsumer(ILogger<CalculateCreditConsumer> logger, ICalculator calculator)
        {
            this.logger = logger;
            this.calculator = calculator;
        }

        public async Task Consume(ConsumeContext<CalculateCreditRequested> context)
        {
            var amount = context.Message.Amount;
            var interestRate = calculator.CalculateInterestRate(context.Message.Interest);
            var period = context.Message.Period;
            var monthlyInstalment = calculator.CalculateMontlyInstalment(amount, interestRate, period);

            var result = calculator.GenerateRepaymentPlan(context.Message.Amount, period, interestRate, monthlyInstalment);

            await context.RespondAsync<CalculateCreditResponse>(new
            {
                MontlyCreditData = result
            });
        }
    }
}
