namespace Contracts
{
    public interface CreateCreditFaulted : IFaultedConsumer
    {
        public string CreditId { get; set; }
    }
}
