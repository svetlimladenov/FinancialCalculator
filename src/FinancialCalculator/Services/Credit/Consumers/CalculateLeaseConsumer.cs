using Contracts;
using Credit.Utils;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Credit.Consumers
{
    public class CalculateLeaseConsumer : IConsumer<CalculateLeaseRequested>
    {
        private readonly ILogger<CalculateCreditConsumer> logger;
        private readonly ICalculator calculator;

        public CalculateLeaseConsumer(ILogger<CalculateCreditConsumer> logger, ICalculator calculator)
        {
            this.logger = logger;
            this.calculator = calculator;
        }

        public async Task Consume(ConsumeContext<CalculateLeaseRequested> context)
        {
            var initialPayment = calculator.CalculateLeaseInitialPayment(context.Message.Amount, context.Message.InitialPaymentPercentage);
            var interestRate = calculator.CalculateInterestRate(context.Message.Interest);

            var remainingAmount = context.Message.Amount - initialPayment;

            var monthlyInstalment = calculator.CalculateMontlyInstalment(remainingAmount, interestRate, context.Message.Period);

            var repaymentPlan = calculator.GenerateRepaymentPlan(remainingAmount, context.Message.Period, interestRate, monthlyInstalment);


            var totalPrice = monthlyInstalment * context.Message.Period + initialPayment;
            var totalIncrease = totalPrice - context.Message.Amount;

            await context.RespondAsync<CalculateLeaseResponse>(new
            {
                Period = context.Message.Period,
                MonthlyInstalment = monthlyInstalment,
                RemainingAmount = remainingAmount,
                InitialPayment = initialPayment,
                RepaymentPlan = repaymentPlan,
                TotalIncrease = totalIncrease,
                TotalPrice = totalPrice
            });
        }
    }
}
