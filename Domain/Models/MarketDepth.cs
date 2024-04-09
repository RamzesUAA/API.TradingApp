namespace Domain.Models
{
    public class MarketDepth
    {
        public string Timestamp { get; set; }
        public string Microtimestamp { get; set; }
        public List<List<decimal>> Bids { get; set; }
        public List<List<decimal>> Asks { get; set; }
    }
}
