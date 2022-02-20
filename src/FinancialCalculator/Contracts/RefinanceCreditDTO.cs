namespace Contracts
{
    public class RefinanceCreditDTO
    {
        public string ParentCreditId { get; set; }

        public string RefinancedCreditId { get; set; }

        public decimal RefinancePaymentAmount { get; set; }
    }
}
