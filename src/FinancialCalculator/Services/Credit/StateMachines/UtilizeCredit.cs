using Automatonymous;
using Contracts;
using MassTransit;
using System;

namespace Credit.StateMachines
{
    public class UtilizeCredit : SagaStateMachineInstance
    {
        public string CreditId { get; set; }

        public string CurrentState { get; set; }

        public Guid CorrelationId { get; set; }

        // Used for request/response
        public Uri ResponseAddress { get; set; }

        public Guid? RequestId { get; set; }

        public UtilizeCreditRequested Request { get; set; }
    }
}
