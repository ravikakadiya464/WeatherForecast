using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Domain.Entities
{
    public class WeatherForecast
    {
        public Guid Id { get; protected set; }

        public int LocationKey { get; protected set; }

        public DateTime ForecastDate { get; protected set; }

        public string AirQuality { get; protected set; }

        public DateTime SunRiseTime { get; protected set; }

        public DateTime SunSetTime { get; protected set; }

        public Guid TemperatureId { get; protected set; }

        public Guid DayId { get; protected set; }

        public Guid NightId { get; protected set; }

        [ForeignKey(nameof(TemperatureId))]
        public virtual Temperature Temperature { get; protected set; }

        [ForeignKey(nameof(DayId))]
        public virtual WeatherForecastDetail Day { get; protected set; }

        [ForeignKey(nameof(NightId))]
        public virtual WeatherForecastDetail Night { get; protected set; }

        protected WeatherForecast()
        {

        }

        public WeatherForecast(Guid id, int locationKey, DateTime forecastDate, string airQuality, DateTime sunRiseTime, DateTime sunSetTime)
        {
            Id = id;
            LocationKey = locationKey;
            ForecastDate = forecastDate;
            AirQuality = airQuality;
            SunRiseTime = sunRiseTime;
            SunSetTime = sunSetTime;
        }

        public WeatherForecast SetTemperature(Guid id, decimal minValue, decimal maxValue, Guid unitOfMeasureId, string phraseForMin = null, string phraseForMax = null)
        {
            Temperature = new Temperature(id, minValue, maxValue, unitOfMeasureId, phraseForMin, phraseForMax);
            return this;
        }

        public WeatherForecast SetDay(Guid id, int icon, string iconPhrase, decimal windSpeed, decimal cloudCover, Guid windSpeedUnitOfMeasureId)
        {
            Day = new WeatherForecastDetail(id, icon, iconPhrase, windSpeed, cloudCover, windSpeedUnitOfMeasureId);
            return this;
        }

        public WeatherForecast SetNight(Guid id, int icon, string iconPhrase, decimal windSpeed, decimal cloudCover, Guid windSpeedUnitOfMeasureId)
        {
            Night = new WeatherForecastDetail(id, icon, iconPhrase, windSpeed, cloudCover, windSpeedUnitOfMeasureId);
            return this;
        }
    }
}
