namespace Contracts
{
    public class RefinanceCreditResponse
    {
        public decimal CreditSum { get; set; }

        public decimal Interest { get; set; }

        public int Months { get; set; }
    }
}