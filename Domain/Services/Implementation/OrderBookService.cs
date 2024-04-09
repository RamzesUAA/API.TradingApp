using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Services.Implementation;

public class OrderBookService : IOrderBookService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _bitstampOrderBookUri;
    private readonly ILogger<OrderBookService> _logger;
    private readonly IAuditLogService _auditLogService;

    public OrderBookService(IHttpClientFactory httpClientFactory,
                            IConfiguration configuration,
                            ILogger<OrderBookService> logger,
                            IAuditLogService auditLogService)
    {
        _httpClientFactory = httpClientFactory;
        _bitstampOrderBookUri = configuration.GetSection("BitstampApi")?.GetValue<string>("OrderBookUri");
        _logger = logger;
        _auditLogService = auditLogService;
    }

    public async Task<MarketDepth> GetOrderBookDataAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(_bitstampOrderBookUri);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var orderBook = JsonConvert.DeserializeObject<MarketDepth>(json);

            await _auditLogService.LogMarketDepthData(orderBook);

            return orderBook;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching order book data");
            throw;
        }
    }
}
