using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.Services;

namespace WeatherForecast.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("/cities")]
        public async Task<IActionResult> GetCities(string city, CancellationToken cancellationToken = default)
        {
            var outcome =
                await _cityService.GetCities(city, cancellationToken);
            return outcome.Item2 is NullGenericError ? Ok(outcome.Item1) : BadRequest(outcome.Item2);
        }
    }
}
