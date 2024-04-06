using Microsoft.AspNetCore.Mvc;

namespace API.TradingApp.Controllers
{
    [Route("order-book")]
    [ApiController]
    public class OrderBookController : Controller
    {
        public OrderBookController()
        {
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("= )");    
        }
    }
}
