using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Persistence;

namespace WeatherForecast.API.StartupConfigurations
{
    public static class SqlServerConfiguration
    {
        public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Default"];

            services.AddDbContext<WeatherForecastContext>(options =>
            {
                options.EnableSensitiveDataLogging();

                options.UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly("WeatherForecast.API");
                    x.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                });
            });

        }
    }
}
