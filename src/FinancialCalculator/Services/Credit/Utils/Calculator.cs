using System;
using System.Collections.Generic;
using Contracts;

namespace Credit.Utils
{
    public class Calculator : ICalculator
    {
        public decimal CalculateInterestRate(decimal interest)
            => interest / 100 / 12;

        public decimal CalculateLeaseInitialPayment(decimal amount, double percentage)
            => amount * (decimal)percentage / 100;


        public decimal CalculateMontlyInstalment(decimal amount, decimal interestRate, int period)
            => Math.Round(amount * (interestRate * (decimal)Math.Pow((double)(1m + interestRate), period) / (decimal)(Math.Pow((double)(1m + interestRate), period) - 1)), 2);

        public List<MonthlyCreditData> GenerateRepaymentPlan(decimal amount, int period, decimal interestRate, decimal monthlyInstalment)
        {
            var leftPrincipal = amount;

            var date = DateTime.Now;

            var result = new List<MonthlyCreditData>();
            for (var i = 0; i < period; i++)
            {
                var monthlyInterest = Math.Round(leftPrincipal * (interestRate), 2);
                var monthlyPrincipal = Math.Round(monthlyInstalment - monthlyInterest, 2);
                leftPrincipal = Math.Round(leftPrincipal - monthlyPrincipal, 2);

                var data = new MonthlyCreditData()
                {
                    Date = date,
                    MonthlyPayment = monthlyInstalment,
                    MonthlyPrincipal = monthlyPrincipal,
                    MonthlyInterest = monthlyInterest,
                    LeftPrincipal = leftPrincipal
                };

                result.Add(data);

                Console.WriteLine($"{date}.' Mesechna vnoska '. {monthlyInstalment}. ' Vnoska glavnica '. {monthlyPrincipal}. ' vnoska lihva '. {monthlyInterest}. ' ostatuk glavnica '. {leftPrincipal};");

                date = date.AddMonths(1);
            }

            return result;
        }
    }
}