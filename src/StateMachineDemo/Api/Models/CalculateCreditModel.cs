namespace Api.Models
{
    public class CalculateCreditModel
    {
        public decimal CreditSum { get; set; }

        public int Months { get; set; }

        public decimal Interest { get; set; }
    }
}