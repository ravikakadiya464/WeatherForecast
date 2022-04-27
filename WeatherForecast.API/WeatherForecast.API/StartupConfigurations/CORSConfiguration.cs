namespace WeatherForecast.API.StartupConfigurations
{
    public static class CORSConfiguration
    {
        public static void UseCORS(this IApplicationBuilder app)
        {
            app.UseCors("default");
        }

        public static void AddCORS(this IServiceCollection services, IConfiguration configuration)
        {
            var corsOrigins = configuration.GetSection("CorsOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("default",
                    builder =>
                    {
                        builder
                            .WithOrigins(corsOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                    });
            });
        }
    }
}
