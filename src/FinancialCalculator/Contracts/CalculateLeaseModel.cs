namespace Contracts
{
    public class CalculateLeaseModel
    {
        public decimal Amount { get; set; }

        public int Period { get; set; }

        public decimal Interest { get; set; }

        public double InitialPaymentPercentage { get; set; }
    }
}