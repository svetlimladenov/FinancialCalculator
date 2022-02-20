using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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
            var amount = context.Message.Amount;
            this.logger.LogDebug("Calculating credit...");
            var interestRate = (context.Message.Interest / 100 / 12); // interest rate is 7.5
            var period = context.Message.Period;
            period = 60; // mesechni vnoski
            var leftPrincipal = amount; // principal is glavnica


            var monthlyPayment = Math.Round(amount * (interestRate * (decimal)Math.Pow((double)(1m + interestRate), period) / (decimal)(Math.Pow((double)(1m + interestRate), period) - 1)), 2);

            var date = DateTime.Now;

            var result = new List<MonthlyCreditData>();
            for (var i = 0; i < period; i++)
            {
                var monthlyInterest = Math.Round(leftPrincipal * (interestRate), 2);
                var monthlyPrincipal = Math.Round(monthlyPayment - monthlyInterest, 2);
                leftPrincipal = Math.Round(leftPrincipal - monthlyPrincipal, 2);


                var data = new MonthlyCreditData()
                {
                    Date = date,
                    MonthlyPayment = monthlyPayment,
                    MonthlyPrincipal = monthlyPrincipal,
                    MonthlyInterest = monthlyInterest,
                    LeftPrincipal = leftPrincipal
                };

                result.Add(data);

                System.Console.WriteLine($"{date}.' Mesechna vnoska '. {monthlyPayment}. ' Vnoska glavnica '. {monthlyPrincipal}. ' vnoska lihva '. {monthlyInterest}. ' ostatuk glavnica '. {leftPrincipal};");

                date = date.AddMonths(1);

            }

            await context.RespondAsync<CalculateCreditResponse>(new
            {
                MontlyCreditData = result
            });
        }
    }
}
