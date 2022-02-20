namespace Contracts
{
    public class CalculateLeaseRequested
    {
        public decimal Amount { get; set; }

        public int Period { get; set; }

        public decimal Interest { get; set; }

        public double InitialPaymentPercentage { get; set; }
    }
}