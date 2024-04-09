using Domain.Models;
using Domain.Services.Interfaces;
using Newtonsoft.Json;
using Persistence.Entities;
using Persistence.Infrastructure;

namespace Domain.Services.Implementation;

public class AuditLogService : IAuditLogService
{
    private readonly IRepository<MarketDepthAuditLog> _repository;

    public AuditLogService(IRepository<MarketDepthAuditLog> repository)
    {
        _repository = repository;
    }

    public async Task LogMarketDepthData(MarketDepth marketDepthData)
    {
        if (marketDepthData == null || string.IsNullOrEmpty(marketDepthData.Timestamp))
        {
            throw new ArgumentException("Invalid market depth data for audit log.");
        }

        var serializedData = JsonConvert.SerializeObject(marketDepthData);
        var auditLog = new MarketDepthAuditLog
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            Data = serializedData
        };

        await _repository.AddAsync(auditLog);
        await _repository.SaveChangesAsync();
    }

    public async Task<MarketDepth> GetMarketDepthDataByIdAsync(Guid id)
    {
        var logEntry = await _repository.GetByIdAsync(id);
        if (logEntry == null)
        {
            return null;
        }
        return JsonConvert.DeserializeObject<MarketDepth>(logEntry.Data);
    }
}
