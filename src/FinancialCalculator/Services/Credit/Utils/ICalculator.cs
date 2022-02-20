using System.Collections.Generic;
using Contracts;

namespace Credit.Utils
{
    public interface ICalculator
    {
        decimal CalculateMontlyInstalment(decimal amount, decimal interestRate, int period);

        decimal CalculateInterestRate(decimal interest);

        decimal CalculateLeaseInitialPayment(decimal amount, double percentage);

        List<MonthlyCreditData> GenerateRepaymentPlan(decimal amount, int period, decimal interestRate, decimal monthlyInstalment);
    }
}
