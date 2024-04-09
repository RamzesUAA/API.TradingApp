using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task LogMarketDepthData(MarketDepth marketDepthData);
        Task<MarketDepth> GetMarketDepthDataByIdAsync(Guid id);
    }
}
