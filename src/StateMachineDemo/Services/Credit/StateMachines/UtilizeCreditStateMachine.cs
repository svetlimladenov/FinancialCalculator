using MassTransit;
using Automatonymous;
using Contracts;
using System.Threading.Tasks;

namespace Credit.StateMachines
{
    public class UtilizeCreditStateMachine : MassTransitStateMachine<UtilizeCredit>
    {
        public UtilizeCreditStateMachine()
        {
            //Declares the property to hold the instance's state as a string (the state name is stored in the property)
            InstanceState(x => x.CurrentState);

            Event(() => UtilizeCreditRequested, x =>
            {
                x.CorrelateBy(i => i.CreditId, m => m.Message.CreateCredit.CreditId);

                x.SelectId(ctx => NewId.NextGuid());

                // Creates a new instance of the saga, and if appropriate, pre-inserts the saga
                // instance to the database. If the saga already exists, any exceptions from the
                // insert are suppressed and processing continues normally.
                x.SetSagaFactory(ctx => new UtilizeCredit()
                {
                    CurrentState = Initial.Name,
                    CreditId = ctx.Message.CreateCredit.CreditId,
                    Request = ctx.Message,
                    ResponseAddress = ctx.ResponseAddress,
                    RequestId = ctx.RequestId
                });
            });

            Event(() => CreateCreditCompleted, x => x.CorrelateBy(i => i.CreditId, m => m.Message.CreditId));
            Event(() => CreateCreditFaulted, x => x.CorrelateBy(i => i.CreditId, m => m.Message.CreditId));
            Event(() => AddBonusPointsCompleted, x => x.CorrelateBy(i => i.CreditId, m => m.Message.CreditId));
            Event(() => AddBonusPointsFaulted, x => x.CorrelateBy(i => i.CreditId, m => m.Message.CreditId));
            Event(() => RefinanceCreditCompleted, x => x.CorrelateBy(i => i.CreditId, m => m.Message.ParentCreditId));
            Event(() => RefinanceCreditFaulted, x => x.CorrelateBy(i => i.CreditId, m => m.Message.ParentCreditId));

            Initially(
               When(UtilizeCreditRequested)
                   .PublishAsync(ctx => ctx.Init<CreateCreditRequested>(new
                   {
                       ctx.Instance.Request.CreateCredit
                   }))
                   .TransitionTo(CreatingCredit));

            During(
                CreatingCredit,
                When(CreateCreditCompleted)
                    .TransitionTo(AddingBonusPoints)
                    .PublishAsync(ctx => ctx.Init<AddBonusPointsRequested>(new { ctx.Instance.Request.BonusPoints })),
                When(CreateCreditFaulted)
                    .SendAsync(ctx => ctx.Instance.ResponseAddress, UtilizeCreditFaultedAsync, PopulateRequestId)
                    .Finalize());

            During(
                AddingBonusPoints,
                When(AddBonusPointsCompleted)
                    .TransitionTo(RefinancingCredit)
                    .PublishAsync(ctx => ctx.Init<RefinanceCreditRequested>(new { ctx.Instance.Request.RefinanceCredit })),
                When(AddBonusPointsFaulted)
                    .SendAsync(ctx => ctx.Instance.ResponseAddress, UtilizeCreditFaultedAsync, PopulateRequestId)
                    .Finalize());

            During(
                RefinancingCredit,
                When(RefinanceCreditCompleted)
                     .SendAsync(ctx => ctx.Instance.ResponseAddress, UtilizeCreditCompletedAsync, PopulateRequestId)
                     .Finalize(),
                When(RefinanceCreditFaulted)
                    .SendAsync(ctx => ctx.Instance.ResponseAddress, UtilizeCreditFaultedAsync, PopulateRequestId)
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        public State CreatingCredit { get; private set; }

        public State AddingBonusPoints { get; private set; }

        public State RefinancingCredit { get; private set; }

        public State ExternalSystemUtilizing { get; private set; }

        public Event<UtilizeCreditRequested> UtilizeCreditRequested { get; private set; }

        public Event<CreateCreditCompleted> CreateCreditCompleted  { get; private set; }

        public Event<CreateCreditFaulted> CreateCreditFaulted { get; private set; }

        public Event<AddBonusPointsCompleted> AddBonusPointsCompleted { get; private set; }
        
        public Event<AddBonusPointsFaulted> AddBonusPointsFaulted { get; private set; }

        public Event<RefinanceCreditCompleted> RefinanceCreditCompleted { get; private set; }

        public Event<RefinanceCreditFaulted> RefinanceCreditFaulted { get; private set; }

        private static void PopulateRequestId<TMessage>(SendContext<TMessage> contextCallback)
            where TMessage : class
        {
            var consumeEventCtx = contextCallback.GetOrAddPayload<ConsumeEventContext<UtilizeCredit>>(() => null);
            contextCallback.RequestId = consumeEventCtx.Instance.RequestId;
        }

        private static Task<UtilizeCreditFaulted> UtilizeCreditFaultedAsync<TMessage>(ConsumeEventContext<UtilizeCredit, TMessage> context)
             where TMessage : class, IFaultedConsumer
        {
            var intValidationErrors = context.Data.ValidationErrors;
            return context.Init<UtilizeCreditFaulted>(new
            {
                context.Instance.CreditId,
                ValidationErrors = intValidationErrors
            });
        }

        private static Task<UtilizeCreditCompleted> UtilizeCreditCompletedAsync<TMessage>(ConsumeEventContext<UtilizeCredit, TMessage> context)
             where TMessage : class
        {
            return context.Init<UtilizeCreditCompleted>(new
            {
                context.Instance.CreditId
            });
        }
    }
}
