using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.TradingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderBookController : ControllerBase
    {
        private readonly IOrderBookService _orderBookService;

        public OrderBookController(IOrderBookService orderBookService)
        {
            _orderBookService = orderBookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var orderBook = await _orderBookService.GetOrderBookDataAsync();
                return Ok(orderBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
