using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.TradingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMarketDepthDataByIdAsync(Guid id)
        {
            var marketDepth = await _auditLogService.GetMarketDepthDataByIdAsync(id);
            if (marketDepth == null)
            {
                return NotFound();
            }
            return Ok(marketDepth);
        }
    }
}
