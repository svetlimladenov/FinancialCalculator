namespace Contracts
{
    public interface AddBonusPointsFaulted : IFaultedConsumer
    {
        public string CreditId { get; set; }
    }
}
