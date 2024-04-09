namespace Persistence.Entities
{
    public class MarketDepthAuditLog : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Data { get; set; }
    }
}
