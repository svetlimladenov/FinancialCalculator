using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ReversePaymentRequested
    {
        string CreditId { get; }

        string PaymentId { get; }

        DateTime OnDate { get; }
    }
}
