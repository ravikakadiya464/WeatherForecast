using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.ViewModels
{
    public class CityViewModel
    {
        public int Key { get; set; }

        public string LocalizedName { get; set; }

        public Location Country { get; set; }

        public Location AdministrativeArea { get; set; }
    }

    public class Location
    {
        public string LocalizedName { get; set; }
    }
}
