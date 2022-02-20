using System.Collections.Generic;

namespace Contracts
{
    public class CalculateLeaseResponse
    {
        public decimal InitialPayment { get; set; }

        public int Period { get; set; }

        public decimal RemainingAmount { get; set; }

        public decimal MonthlyInstalment { get; set; }

        public decimal TotalIncrease { get; set; }

        public decimal TotalPrice { get; set; }

        public List<MonthlyCreditData> RepaymentPlan { get; set; } = new List<MonthlyCreditData>();
    }
}