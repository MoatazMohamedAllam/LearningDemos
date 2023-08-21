namespace IocContainer_Implementation
{
    internal class RandomGuidGenerator
    {
        public Guid RandomGuid { get; } = Guid.NewGuid();
    }
}