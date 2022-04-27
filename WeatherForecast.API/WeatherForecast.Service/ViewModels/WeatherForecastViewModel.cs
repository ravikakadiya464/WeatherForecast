using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.ViewModels
{
    public class WeatherForecastViewModel
    {
        public Guid Id { get; set; }

        public DateTime ForecastDate { get; set; }

        public string AirQuality { get; set; }

        public bool IsInCurrentWeek { get; set; }

        public bool IsToday { get; set; }

        public TemperatureViewModel Temperature { get; set; }

        public WeatherForecastDetailViewModel ForecastDetail { get; set; }
    }
}
