using BlockCountriesTask.IServices;
using BlockCountriesTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockCountriesTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpController : ControllerBase
    {
        private readonly IIpLookupService _ipLookupService;
        private readonly ICountryBlockService _countryBlockService;
        private readonly IBlockedLogService _blockedLogService;

        public IpController(IIpLookupService ipLookupService,
            ICountryBlockService countryBlockService,
            IBlockedLogService  blockedLogService)
        {
            _ipLookupService = ipLookupService;
            _countryBlockService = countryBlockService;
            _blockedLogService = blockedLogService;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup([FromQuery] string ipAddress)
        {
             var result = await _ipLookupService.LookupIpAsync(ipAddress);
             return Ok(result);
        }
        [HttpGet("check-block")]
        public async Task<IActionResult> CheckBlock()
        {
                var result = await _ipLookupService.LookupIpAsync(null);
                var isBlocked = _countryBlockService.IsBlocked(result.CountryCode);
                var userAgent = Request.Headers["User-Agent"].ToString();
                var log = new BlockedAttemptLog
                {
                    IpAddress = result.Ip,
                    Timestamp = DateTime.UtcNow,
                    CountryCode = result.CountryCode,
                    IsBlocked = isBlocked,
                    UserAgent = userAgent
                };
                _blockedLogService.Log(log);
                return Ok(new
                {
                    result.Ip,
                    result.CountryCode,
                    result.CountryName,
                    IsBlocked = isBlocked
                });
        }



    }
}
