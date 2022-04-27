using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.ViewModels
{
    public class UnitOfMeasureViewModel
    {
        public Guid Id { get; set; }

        public string Unit { get; set; }

        public int UnitType { get; set; }
    }
}
