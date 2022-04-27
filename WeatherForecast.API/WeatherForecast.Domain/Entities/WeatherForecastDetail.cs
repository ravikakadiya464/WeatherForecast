using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Domain.Entities
{
    public class WeatherForecastDetail
    {
        public Guid Id { get; protected set; }

        [Required]
        public int Icon { get; protected set; }



        [Required]
        public string IconPhrase { get; protected set; }

        [Required]
        public decimal WindSpeed { get; protected set; }

        [Required]
        public decimal CloudCover { get; protected set; }

        [Required]
        public Guid WindSpeedUnitId { get; protected set; }

        [ForeignKey(nameof(WindSpeedUnitId))]
        public virtual UnitOfMeasure WindSpeedUnit { get; protected set; }

        protected WeatherForecastDetail()
        {

        }

        public WeatherForecastDetail(Guid id, int icon, string iconPhrase, decimal windSpeed, decimal cloudCover, Guid windSpeedUnitId)
        {
            Id = id;
            Icon = icon;
            IconPhrase = iconPhrase;
            WindSpeed = windSpeed;
            CloudCover = cloudCover;
            WindSpeedUnitId = windSpeedUnitId;
        }
    }
}
