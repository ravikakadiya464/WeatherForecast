using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WeatherForecast.Service.AppSettings;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.Services;
using WeatherForecast.Service.ViewModels;
using Xunit;

namespace WeatherForecast.Service.Test
{
    public class CityServiceTest
    {
        [Fact]
        public async Task Get_Cities_Ok_MustPass()
        {
            var accuWeatherApiSettingsMock = new Mock<IOptions<AccuWeatherApiSettings>>();
            var httpClientMock = Mock.Of<HttpClient>();

            accuWeatherApiSettingsMock.Setup(accuWeatherApiSettingsOptionMock => accuWeatherApiSettingsOptionMock.Value)
                .Returns(new AccuWeatherApiSettings
                {
                    ApiKey = "S8PP36xP6xj3Xw9ofo3p32CYi0XL6ODs",
                    BaseUrl = "http://dataservice.accuweather.com"
                });

            var cityService = new CityService(
                accuWeatherApiSettingsMock.Object, httpClientMock);

            var result = await cityService.GetCities("San Diago", CancellationToken.None);

            Assert.IsNotType<GenericError>(result.Item2);
            Assert.IsType<List<CityViewModel>>(result.Item1);
        }
    }
}
