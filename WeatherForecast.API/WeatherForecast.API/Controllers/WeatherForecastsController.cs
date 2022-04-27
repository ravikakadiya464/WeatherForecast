using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.Services;

namespace WeatherForecast.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastsController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("/cities/{locationKey}/weather-forecasts")]
        public async Task<IActionResult> GetForecasts(int locationKey, CancellationToken cancellationToken = default)
        {
            var outcome =
                await _weatherForecastService.GetForecasts(locationKey, cancellationToken);
            return outcome.Item2 is NullGenericError ? Ok(outcome.Item1) : BadRequest(outcome.Item2);
        }
    }
}