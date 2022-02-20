namespace Contracts
{
    public class CalculateCreditRequested
    {
        public decimal Amount { get; set; }

        public int Period { get; set; }

        public decimal Interest { get; set; }
    }

    public class CalculateRefinanceModel
    {
        public decimal CreditSum { get; set; }

        public decimal Interest { get; set; }

        public int Months { get; set; }

        public int PaymentsMade { get; set; }
    }

    public class CalculateRefinanceRequested
    {
        public decimal CreditSum { get; set; }

        public decimal Interest { get; set; }

        public int Months { get; set; }

        public int PaymentsMade { get; set; }
    }

    public class RefinanceCreditResponse
    {
        public decimal CreditSum { get; set; }

        public decimal Interest { get; set; }

        public int Months { get; set; }
    }
}