using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IOrderBookService
    {
        Task<MarketDepth> GetOrderBookDataAsync();
    }
}
