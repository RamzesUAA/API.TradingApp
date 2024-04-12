using Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace API.TradingApp.Hubs
{
    public class OrderBookHub : Hub
    {
        public async Task BroadcastOrderBookUpdate(MarketDepth orderBook)
        {
            await Clients.All.SendAsync("ReceiveOrderBookUpdate", orderBook);
        }
    }
}
