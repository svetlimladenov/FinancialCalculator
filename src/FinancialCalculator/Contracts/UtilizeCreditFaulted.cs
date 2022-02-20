using System.Collections.Generic;

namespace Contracts
{
    public interface UtilizeCreditFaulted
    {
        public string CreditId { get; set; }

        public List<int> ValidationErrors { get; set; }
    }
}
