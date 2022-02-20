using System;

namespace Contracts
{
    public class MonthlyCreditData
    {
        public DateTime Date { get; set; }

        public decimal MonthlyPayment { get; set; }

        public decimal MonthlyPrincipal { get; set; }

        public decimal MonthlyInterest { get; set; }

        public decimal LeftPrincipal { get; set; }
    }
}