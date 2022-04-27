using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Service.ViewModels;
using UnitOfMeasure = WeatherForecast.Domain.Entities.UnitOfMeasure;

namespace WeatherForecast.Service.AutoMapper
{
    public class WeatherForecastMapping
    {
        public static void Map(AutoMapperProfile profile)
        {
            profile.CreateMap<Domain.Entities.WeatherForecast, WeatherForecastViewModel>()
                .ForMember(x => x.IsInCurrentWeek,
                    opt =>
                        opt.MapFrom(f => IsInCurrentWeek(f.ForecastDate))
                )
                .ForMember(x => x.IsToday,
                    opt =>
                        opt.MapFrom(f => IsToday(f.ForecastDate))
                )
                .ForMember(x => x.ForecastDetail,
                    opt =>
                        opt.MapFrom(f => IsDay(f.SunRiseTime, f.SunSetTime) ? f.Day : f.Night)
                );

            profile.CreateMap<Domain.Entities.Temperature, TemperatureViewModel>();
            profile.CreateMap<Domain.Entities.WeatherForecastDetail, WeatherForecastDetailViewModel>();
            profile.CreateMap<UnitOfMeasure, UnitOfMeasureViewModel>();
        }

        private static bool IsInCurrentWeek(DateTime date)
        {
            var currentDate = DateTime.Now.Date;
            var thisWeekStart = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            if (date >= thisWeekStart && date <= thisWeekEnd)
                return true;
            return false;
        }

        private static bool IsToday(DateTime date)
        {
            var currentDate = DateTime.Now.Date;

            if (date == currentDate)
                return true;
            return false;
        }

        private static bool IsDay(DateTime sunRiseTime, DateTime sunSetTime)
        {
            var currentTime = DateTime.Now;

            if (currentTime >= sunRiseTime && currentTime <= sunSetTime)
                return true;
            return false;
        }
    }
}
