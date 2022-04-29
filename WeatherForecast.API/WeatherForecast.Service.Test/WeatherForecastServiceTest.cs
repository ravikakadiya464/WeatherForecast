using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Repository.Repositories;
using WeatherForecast.Service.AppSettings;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.Services;
using WeatherForecast.Service.ViewModels;
using Xunit;
using UnitOfMeasure = WeatherForecast.Domain.Entities.UnitOfMeasure;

namespace WeatherForecast.Service.Test
{
    public class WeatherForecastServiceTest
    {
        [Fact]
        public async Task Get_Forecasts_From_Persistence_Ok_MustPass()
        {
            var accuWeatherApiSettingsMock = new Mock<IOptions<AccuWeatherApiSettings>>();
            var mapperMock = new Mock<IMapper>();
            var httpClientMock = Mock.Of<HttpClient>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var forecast = new Domain.Entities.WeatherForecast(Guid.NewGuid(), 190262, DateTime.Now.Date, "Good", DateTime.Now, DateTime.Now.AddHours(7));
            var listForecast = new List<Domain.Entities.WeatherForecast>() { forecast };

            unitOfWorkMock.Setup(repo =>
                    repo.WeatherForecastRepository.GetListAsync(It.IsAny<List<Expression<Func<Domain.Entities.WeatherForecast, bool>>>>(), 
                        It.IsAny<List<Expression<Func<Domain.Entities.WeatherForecast, object>>>>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<Func<IQueryable<Domain.Entities.WeatherForecast>, IOrderedQueryable<Domain.Entities.WeatherForecast>>>(), 
                        CancellationToken.None))
                .ReturnsAsync(listForecast);

            mapperMock.Setup(x => x.Map<List<WeatherForecastViewModel>>(It.IsAny<List<Domain.Entities.WeatherForecast>>()))
                .Returns(new List<WeatherForecastViewModel>() { new WeatherForecastViewModel() });

            var forecastService = new WeatherForecastService(
                accuWeatherApiSettingsMock.Object, mapperMock.Object, unitOfWorkMock.Object, httpClientMock);

            var result = await forecastService.GetForecasts(202441);

            Assert.IsNotType<GenericError>(result.Item2);
            Assert.True(result.Item1.Count > 0);
        }

        [Fact]
        public async Task Get_Forecasts_From_Accu_Weather_Ok_MustPass()
        {
            var accuWeatherApiSettingsMock = new Mock<IOptions<AccuWeatherApiSettings>>();
            var mapperMock = new Mock<IMapper>();
            var httpClientMock = Mock.Of<HttpClient>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var forecast = new Domain.Entities.WeatherForecast(Guid.NewGuid(), 190262, DateTime.Now.Date, "Good", DateTime.Now, DateTime.Now.AddHours(7));
            var listForecast = new List<Domain.Entities.WeatherForecast>(){ forecast };
            var unitOfMeasure = new List<UnitOfMeasure>() { new UnitOfMeasure(Guid.NewGuid(), "C", 17) };


            unitOfWorkMock.SetupSequence(repo =>
                    repo.WeatherForecastRepository.GetListAsync(It.IsAny<List<Expression<Func<Domain.Entities.WeatherForecast, bool>>>>(),
                        It.IsAny<List<Expression<Func<Domain.Entities.WeatherForecast, object>>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<Func<IQueryable<Domain.Entities.WeatherForecast>, IOrderedQueryable<Domain.Entities.WeatherForecast>>>(),
                        CancellationToken.None))
                .ReturnsAsync(new List<Domain.Entities.WeatherForecast>())
                .ReturnsAsync(listForecast);

            unitOfWorkMock.Setup(repo =>
                    repo.UnitOfMeasureRepository.GetListAsync(It.IsAny<List<Expression<Func<UnitOfMeasure, bool>>>>(),
                        It.IsAny<List<Expression<Func<UnitOfMeasure, object>>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<Func<IQueryable<UnitOfMeasure>, IOrderedQueryable<UnitOfMeasure>>>(),
                        CancellationToken.None))
                .ReturnsAsync(unitOfMeasure);

            unitOfWorkMock.Setup(repo =>
                repo.WeatherForecastRepository.InsertRangeAsync(It.IsAny<List<Domain.Entities.WeatherForecast>>(),
                    CancellationToken.None));

            unitOfWorkMock.Setup(repo => repo.CommitChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));

            mapperMock.Setup(x => x.Map<List<WeatherForecastViewModel>>(It.IsAny<List<Domain.Entities.WeatherForecast>>()))
                .Returns(new List<WeatherForecastViewModel>(){ new WeatherForecastViewModel()});

            accuWeatherApiSettingsMock.Setup(accuWeatherApiSettingsOptionMock => accuWeatherApiSettingsOptionMock.Value)
                .Returns(new AccuWeatherApiSettings
                {
                    ApiKey = "Api_Key",
                    BaseUrl = "Url"
                });

            var forecastService = new WeatherForecastService(
                accuWeatherApiSettingsMock.Object, mapperMock.Object, unitOfWorkMock.Object, httpClientMock);

            var result = await forecastService.GetForecasts(202441);

            Assert.IsNotType<GenericError>(result.Item2);
            Assert.True(result.Item1.Count > 0);
        }

        [Fact]
        public async Task Get_Forecast_ErrorCode_MustPass()
        {
            var accuWeatherApiSettingsMock = new Mock<IOptions<AccuWeatherApiSettings>>();
            var mapperMock = new Mock<IMapper>();
            var httpClientMock = Mock.Of<HttpClient>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var forecast = new Domain.Entities.WeatherForecast(Guid.NewGuid(), 190262, DateTime.Now.Date, "Good", DateTime.Now, DateTime.Now.AddHours(7));
            var listForecast = new List<Domain.Entities.WeatherForecast>() { forecast };
            var unitOfMeasure = new List<UnitOfMeasure>() { new UnitOfMeasure(Guid.NewGuid(), "C", 17) };


            unitOfWorkMock.SetupSequence(repo =>
                    repo.WeatherForecastRepository.GetListAsync(It.IsAny<List<Expression<Func<Domain.Entities.WeatherForecast, bool>>>>(),
                        It.IsAny<List<Expression<Func<Domain.Entities.WeatherForecast, object>>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<Func<IQueryable<Domain.Entities.WeatherForecast>, IOrderedQueryable<Domain.Entities.WeatherForecast>>>(),
                        CancellationToken.None))
                .ReturnsAsync(new List<Domain.Entities.WeatherForecast>())
                .ReturnsAsync(listForecast);

            unitOfWorkMock.Setup(repo =>
                    repo.UnitOfMeasureRepository.GetListAsync(It.IsAny<List<Expression<Func<UnitOfMeasure, bool>>>>(),
                        It.IsAny<List<Expression<Func<UnitOfMeasure, object>>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<Func<IQueryable<UnitOfMeasure>, IOrderedQueryable<UnitOfMeasure>>>(),
                        CancellationToken.None))
                .ReturnsAsync(unitOfMeasure);

            unitOfWorkMock.Setup(repo =>
                repo.WeatherForecastRepository.InsertRangeAsync(It.IsAny<List<Domain.Entities.WeatherForecast>>(),
                    CancellationToken.None));

            unitOfWorkMock.Setup(repo => repo.CommitChangesAsync(CancellationToken.None)).Returns(Task.FromResult(0));

            mapperMock.Setup(x => x.Map<List<WeatherForecastViewModel>>(It.IsAny<List<Domain.Entities.WeatherForecast>>()))
                .Returns(new List<WeatherForecastViewModel>());

            accuWeatherApiSettingsMock.Setup(accuWeatherApiSettingsOptionMock => accuWeatherApiSettingsOptionMock.Value)
                .Returns(new AccuWeatherApiSettings
                {
                    ApiKey = "Api_Key",
                    BaseUrl = "Url"
                });

            var forecastService = new WeatherForecastService(
                accuWeatherApiSettingsMock.Object, mapperMock.Object, unitOfWorkMock.Object, httpClientMock);

            var result = await forecastService.GetForecasts(202441);

            Assert.IsType<GenericError>(result.Item2);
            Assert.True(result.Item1.Count == 0);
        }
    }
}
