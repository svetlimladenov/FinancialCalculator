namespace Api.Models
{
    public class CreateCreditModel
    {
        public string CreditId { get; set; }

        public int Amount { get; set; }

        public int Period { get; set; }
    }
}
