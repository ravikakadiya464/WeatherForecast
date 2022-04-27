using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.ViewModels
{
    public class WeatherForecastResponse
    {
        public List<DailyForecast> DailyForecasts { get; set; }
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }

        public Temperature Temperature { get; set; }

        public WeatherForecastDetail Day { get; set; }

        public WeatherForecastDetail Night { get; set; }

        public Sun Sun { get; set; }

        public List<AirAndPollen> AirAndPollen { get; set; }
    }

    public class Temperature
    {
        public UnitOfMeasure Maximum { get; set; }

        public UnitOfMeasure Minimum { get; set; }
    }

    public class UnitOfMeasure
    {
        public decimal Value { get; set; }

        public string Unit { get; set; }

        public int UnitType { get; set; }

        public string Phrase { get; set; }
    }

    public class WeatherForecastDetail
    {
        public int Icon { get; set; }

        public string IconPhrase { get; set; }

        public decimal CloudCover { get; set; }

        public WindResponse Wind { get; set; }
    }

    public class WindResponse
    {
        public UnitOfMeasure Speed { get; set; }
    }

    public class AirAndPollen
    {
        public string Name { get; set; }

        public string Category { get; set; }
    }

    public class Sun
    {
        public DateTime Rise { get; set; }

        public DateTime Set { get; set; }
    }
}
