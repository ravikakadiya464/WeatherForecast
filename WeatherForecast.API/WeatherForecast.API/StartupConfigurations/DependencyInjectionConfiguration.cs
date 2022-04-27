using WeatherForecast.Repository.Repositories;
using WeatherForecast.Service.Services;

namespace WeatherForecast.API.StartupConfigurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<ICityService, CityService>();

            services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
