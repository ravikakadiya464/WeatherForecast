using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.ViewModels
{
    public class WeatherForecastDetailViewModel
    {
        public Guid Id { get; set; }

        public int Icon { get; set; }

        public string IconPhrase { get; set; }

        public decimal WindSpeed { get; set; }

        public decimal CloudCover { get; set; }

        public UnitOfMeasureViewModel WindSpeedUnit { get; set; }
    }
}
