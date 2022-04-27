using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.ViewModels
{
    public class TemperatureViewModel
    {
        public Guid Id { get; set; }

        public decimal MinValue { get; set; }

        public decimal MaxValue { get; set; }

        public string PhraseForMin { get; set; }

        public string PhraseForMax { get; set; }

        public UnitOfMeasureViewModel UnitMeasure { get; set; }
    }
}
