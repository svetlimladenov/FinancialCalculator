using Contracts;

namespace Api.Models
{
    public class UtilizeCreditModel
    {
        public CreateCreditModel CreateCredit { get; set; }

        public BonusPointsModel BonusPoints { get; set; }

        public RefinanceCreditModel RefinanceCredit { get; set; }
    }
}
