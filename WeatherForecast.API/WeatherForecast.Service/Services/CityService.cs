using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using WeatherForecast.Service.AppSettings;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.ViewModels;

namespace WeatherForecast.Service.Services
{
    public class CityService : ICityService
    {
        private readonly AccuWeatherApiSettings _accuWeatherApiSettings;
        private readonly HttpClient _httpClient;

        private const string ERROR_GET_CITIES = "ERROR_GET_CITIES";

        public CityService(IOptions<AccuWeatherApiSettings> accuWeatherApiSettingsOptions, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _accuWeatherApiSettings = accuWeatherApiSettingsOptions.Value;
        }

        public async Task<(List<CityViewModel>, GenericError)> GetCities(string city, CancellationToken cancellationToken = default)
        {
            try
            {
                var cityList = await _httpClient.GetFromJsonAsync<List<CityViewModel>>($"{_accuWeatherApiSettings.BaseUrl}/locations/v1/cities/autocomplete?apikey={_accuWeatherApiSettings.ApiKey}&q={city}", cancellationToken);
                return (cityList, new NullGenericError());
            }
            catch (Exception ex)
            {
                var error = new GenericError("There was an error while listing cities", ERROR_GET_CITIES, ex);
                return (new List<CityViewModel>(), error);
            }
        }
    }
}
