namespace Contracts
{
    public class CalculateRefinanceRequested
    {
        public decimal CreditSum { get; set; }

        public decimal Interest { get; set; }

        public int Months { get; set; }

        public int PaymentsMade { get; set; }
    }
}