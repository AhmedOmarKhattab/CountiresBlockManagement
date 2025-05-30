using BlockCountriesTask.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockCountriesTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IBlockedLogService _blockedLogService;

        public LogsController(IBlockedLogService blockedLogService)
        {
            _blockedLogService = blockedLogService;
        }
        [HttpGet("blocked-attempts")]
        public IActionResult GetLogs( [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var logs = _blockedLogService.GetAll(pageIndex, pageSize);
            return Ok(logs);
        }


    }
}
