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
    public class CitiesControllerTest
    {
        [Fact]
        public async Task List_Cities_Ok_MustPass()
        {
            var cityServiceMock = new Mock<ICityService>();
            var cityResponseModel = new List<CityViewModel>
            {
                new CityViewModel
                {
                    Key = 202441
                }
            };

            cityServiceMock.Setup(service =>
                    service.GetCities(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync((cityResponseModel, new NullGenericError()));

            var citiesController = new CitiesController(cityServiceMock.Object);
            var result = await citiesController.GetCities("Surat", CancellationToken.None);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsNotType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task List_Cities_BadResult_MustPass()
        {
            var cityServiceMock = new Mock<ICityService>();

            cityServiceMock.Setup(service =>
                    service.GetCities(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync((new List<CityViewModel>(), new GenericError("There was an error while listing cities", "ERROR_GET_CITIES")));

            var citiesController = new CitiesController(cityServiceMock.Object);
            var result = await citiesController.GetCities("Surat", CancellationToken.None);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsNotType<OkObjectResult>(result);
        }
    }
}
