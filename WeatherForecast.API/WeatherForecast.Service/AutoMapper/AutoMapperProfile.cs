using AutoMapper;

namespace WeatherForecast.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            WeatherForecastMapping.Map(this);
        }
    }
}
