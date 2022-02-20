namespace Contracts
{
    public interface RefinanceCreditFaulted : IFaultedConsumer
    {
        public string ParentCreditId { get; set; }
    }
}
