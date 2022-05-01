using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using WeatherForecast.Repository.Repositories;
using WeatherForecast.Service.AppSettings;
using WeatherForecast.Service.Builders;
using WeatherForecast.Service.Errors;
using WeatherForecast.Service.ViewModels;

namespace WeatherForecast.Service.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly AccuWeatherApiSettings _accuWeatherApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private const string ERR_ADD_WEATHER_FORECAST = "ERR_ADD_WEATHER_FORECAST";
        private const string ERR_GET_WEATHER_FORECAST = "ERR_GET_WEATHER_FORECAST";

        public WeatherForecastService(IOptions<AccuWeatherApiSettings> accuWeatherApiSettingsOptions, IMapper mapper, IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _accuWeatherApiSettings = accuWeatherApiSettingsOptions.Value;
            _httpClient = httpClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<(List<WeatherForecastViewModel>, GenericError)> GetForecasts(int locationKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var currentDate = DateTime.Now.Date;
                var startDate = currentDate.AddDays(-((int)currentDate.DayOfWeek));
                var endDate = startDate.AddDays(7).AddSeconds(-1);

                var predicates =
                    new List<Expression<Func<Domain.Entities.WeatherForecast, bool>>>()
                    {
                        x => x.LocationKey == locationKey &&
                        x.ForecastDate.Date >= startDate && x.ForecastDate.Date <= endDate.Date
                    };

                var forecast = await _unitOfWork.WeatherForecastRepository.GetListAsync(predicates,
                    orderBy: x => x.OrderBy(x => x.ForecastDate), cancellationToken: cancellationToken);

                if (forecast.All(x => x.ForecastDate != currentDate))
                {
                    var forecasts = await _httpClient.GetFromJsonAsync<WeatherForecastResponse>($"{_accuWeatherApiSettings.BaseUrl}/forecasts/v1/daily/5day/{locationKey}?apikey={_accuWeatherApiSettings.ApiKey}&details=true&metric=true", cancellationToken);

                    if (forecasts != null)
                    {
                        var convertedForecasts = await WeatherForecastBuilder.Build(_unitOfWork, forecasts, locationKey);
                        await _unitOfWork.WeatherForecastRepository.InsertRangeAsync(convertedForecasts, cancellationToken);
                        var result = await _unitOfWork.CommitChangesAsync(cancellationToken);

                        if (result == 0)
                        {
                            return (new List<WeatherForecastViewModel>(), new GenericError("There was an error while adding forecast.", ERR_ADD_WEATHER_FORECAST));
                        }
                    }
                }

                var includes = new List<Expression<Func<Domain.Entities.WeatherForecast, object>>>()
                {
                    x => x.Temperature.UnitMeasure,
                    x => x.Day.WindSpeedUnit,
                    x => x.Night.WindSpeedUnit
                };

                var thisWeekForecasts = await _unitOfWork.WeatherForecastRepository.GetListAsync(predicates, includes,
                    orderBy: x => x.OrderBy(x => x.ForecastDate), cancellationToken: cancellationToken);
                var mappedItems = _mapper.Map<List<WeatherForecastViewModel>>(thisWeekForecasts);

                return (mappedItems, new NullGenericError());
            }
            catch (Exception ex)
            {
                var error = new GenericError("There was an error while listing forecast.", ERR_GET_WEATHER_FORECAST, ex);
                return (new List<WeatherForecastViewModel>(), error);
            }
        }
    }
}
