using WeatherForecast.Service.AppSettings;

namespace WeatherForecast.API.StartupConfigurations
{
    public static class AppSettingsConfiguration
    {
        public static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccuWeatherApiSettings>(options =>
            {
                options.BaseUrl = configuration["WeatherApiSettings:BaseUrl"];
                options.ApiKey = configuration["WeatherApiSettings:ApiKey"];
            });

        }
    }
}
