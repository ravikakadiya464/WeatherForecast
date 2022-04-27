using WeatherForecast.Service.Errors;
using WeatherForecast.Service.ViewModels;

namespace WeatherForecast.Service.Services
{
    public interface ICityService
    {
        Task<(List<CityViewModel>, GenericError)> GetCities(string city, CancellationToken cancellationToken = default);
    }
}
