using BlockCountriesTask.Dtos;
using BlockCountriesTask.IServices;
using BlockCountriesTask.Response;
using BlockCountriesTask.SpecParams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockCountriesTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryBlockService _countryBlockService;
        private readonly ITemporalBlockService _temporalBlockService;

        public CountriesController(ICountryBlockService countryBlockService,
            ITemporalBlockService temporalBlockService)
        {
            this._countryBlockService = countryBlockService;
            this._temporalBlockService = temporalBlockService;
        }
        [HttpPost("block")]
        public ActionResult AddCountry(CountryDto countryDto)
        {
           var result= _countryBlockService.Add(countryDto);
            if(result)
                return Ok(new ApiResponse(200));

            return BadRequest(new ApiResponse(400));
        }

        [HttpDelete("block/{countryCode}")]
        public ActionResult DeleteCountry(string countryCode)
        {
            var result = _countryBlockService.Remove(countryCode);
            if (result)
                return Ok(new ApiResponse(200));

            return BadRequest(new ApiResponse(404));
        }
        [HttpGet("blocked")]
        public ActionResult GetAllBlockedCountries([FromQuery] CountrySpecParams specParams)
        {
            var result = _countryBlockService.GetAll(specParams);
            return Ok(result);  
        }
        [HttpPost("temporal-block")]
        public IActionResult AddTemporalBlock(  [FromBody] TemporalBlockRequestDto request)
        {
            if (_temporalBlockService.AddTemporaryBlock(request.CountryCode, request.DurationMinutes, out var error))
                return Ok(new { Message = "Temporary block added." });

            return Conflict(new { Error = error });
        }


    }
}
