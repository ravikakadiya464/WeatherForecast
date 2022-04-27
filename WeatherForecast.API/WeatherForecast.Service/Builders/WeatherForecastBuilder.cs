using WeatherForecast.Repository.Repositories;
using WeatherForecast.Service.ViewModels;

namespace WeatherForecast.Service.Builders
{
    public class WeatherForecastBuilder
    {
        public static async Task<List<Domain.Entities.WeatherForecast>> Build(IUnitOfWork unitOfWork, WeatherForecastResponse model, int locationKey)
        {
            var unitOfMeasures = await unitOfWork.UnitOfMeasureRepository.GetListAsync();

            List<Domain.Entities.WeatherForecast> forecasts = new List<Domain.Entities.WeatherForecast>();
            foreach (var dailyForecast in model.DailyForecasts)
            {
                var temperatureUnit = unitOfMeasures.FirstOrDefault(x => x.UnitType == dailyForecast.Temperature.Minimum.UnitType);

                var windSpeedUnit = unitOfMeasures.FirstOrDefault(x => x.UnitType == dailyForecast.Day.Wind.Speed.UnitType);
                var airQuality = dailyForecast.AirAndPollen.FirstOrDefault(x => x.Name == "AirQuality")?.Category;

                var forecast = new Domain.Entities.WeatherForecast(Guid.NewGuid(), locationKey, dailyForecast.Date.Date, airQuality, dailyForecast.Sun.Rise, dailyForecast.Sun.Set);
                if (temperatureUnit != null)
                    forecast.SetTemperature(Guid.NewGuid(), dailyForecast.Temperature.Minimum.Value,
                        dailyForecast.Temperature.Maximum.Value, temperatureUnit.Id);

                if (windSpeedUnit != null)
                {
                    forecast.SetDay(Guid.NewGuid(), dailyForecast.Day.Icon, dailyForecast.Day.IconPhrase,
                        dailyForecast.Day.Wind.Speed.Value, dailyForecast.Day.CloudCover,
                        windSpeedUnit.Id);

                    forecast.SetNight(Guid.NewGuid(), dailyForecast.Night.Icon, dailyForecast.Night.IconPhrase,
                        dailyForecast.Night.Wind.Speed.Value, dailyForecast.Night.CloudCover,
                        windSpeedUnit.Id);
                }

                forecasts.Add(forecast);
            }

            return forecasts;
        }
    }
}
