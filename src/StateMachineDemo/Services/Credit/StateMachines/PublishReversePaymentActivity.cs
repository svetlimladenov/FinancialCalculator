using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Automatonymous;
using Contracts;
using GreenPipes;
using Infrastucture;
using MassTransit;

namespace Credit.StateMachines
{
    public class PublishReversePaymentActivity : Activity<UtilizeCredit, AddBonusPointsCompleted>
    {
        readonly ConsumeContext _context;
        private readonly IDateTimeProvider dateTimeProvider;

        public PublishReversePaymentActivity(ConsumeContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<UtilizeCredit, AddBonusPointsCompleted> context, Behavior<UtilizeCredit, AddBonusPointsCompleted> next)
        {
            await this._context.Publish<ReversePaymentRequested>(new 
            { 
                context.Data.CreditId, 
                PaymentId = 5, 
                OnDate = dateTimeProvider.GetDate()
            });

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<UtilizeCredit, AddBonusPointsCompleted, TException> context, Behavior<UtilizeCredit, AddBonusPointsCompleted> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("publish-reverse-payment");
        }
    }
}
