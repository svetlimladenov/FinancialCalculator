using System;

namespace Contracts
{
    public interface UtilizeCreditRequested
    {
        public CreateCreditDTO CreateCredit { get; set; }

        public BonusPointsDTO BonusPoints { get; set; }

        public RefinanceCreditDTO RefinanceCredit { get; set; }
    }
}
