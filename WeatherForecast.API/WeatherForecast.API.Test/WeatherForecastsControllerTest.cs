using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherForecast.API.Controllers;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.Services;
using WeatherForecast.Service.ViewModels;
using Xunit;

namespace WeatherForecast.API.Test
{
    public class WeatherForecastsControllerTest
    {
        [Fact]
        public async Task List_Forecast_Ok_MustPass()
        {
            var forecastServiceMock = new Mock<IWeatherForecastService>();
            var forecastDisplayModel = new List<WeatherForecastViewModel>
            {
                new WeatherForecastViewModel
                {
                    Id = Guid.NewGuid()
                }
            };

            forecastServiceMock.Setup(service =>
                    service.GetForecasts(It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync((forecastDisplayModel, new NullGenericError()));

            var forecastsController = new WeatherForecastsController(forecastServiceMock.Object);
            var result = await forecastsController.GetForecasts(202441, CancellationToken.None);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsNotType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task List_Forecast_BadResult_MustPass()
        {
            var forecastServiceMock = new Mock<IWeatherForecastService>();

            forecastServiceMock.Setup(service =>
                    service.GetForecasts(It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync((new List<WeatherForecastViewModel>(), new GenericError("There was an error while listing cities", "ERROR_GET_CITIES")));

            var forecastsController = new WeatherForecastsController(forecastServiceMock.Object);
            var result = await forecastsController.GetForecasts(202441, CancellationToken.None);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsNotType<OkObjectResult>(result);
        }
    }
}
