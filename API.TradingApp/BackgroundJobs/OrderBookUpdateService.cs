using API.TradingApp.Hubs;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class OrderBookUpdateService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<OrderBookUpdateService> _logger;
    private readonly IHubContext<OrderBookHub> _hubContext;

    public OrderBookUpdateService(IServiceScopeFactory serviceScopeFactory, ILogger<OrderBookUpdateService> logger, IHubContext<OrderBookHub> hubContext)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var orderBookService = scope.ServiceProvider.GetRequiredService<IOrderBookService>();

                try
                {
                    var orderBook = await orderBookService.GetOrderBookDataAsync();

                    await _hubContext.Clients.All.SendAsync("ReceiveOrderBookUpdate", orderBook, cancellationToken: stoppingToken);

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching or broadcasting order book data");
                }
            }
        }
    }
}
