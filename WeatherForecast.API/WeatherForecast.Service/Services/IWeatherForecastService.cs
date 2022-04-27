using WeatherForecast.Service.Errors;
using WeatherForecast.Service.ViewModels;

namespace WeatherForecast.Service.Services
{
    public interface IWeatherForecastService
    {
        Task<(List<WeatherForecastViewModel>, GenericError)> GetForecasts(int locationKey, CancellationToken cancellationToken = default);
    }
}
