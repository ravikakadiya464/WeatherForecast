using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.EFConfigurations;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain.Persistence
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options)
           : base(options)
        {

        }

        public DbSet<Entities.WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<Temperature> Temperatures { get; set; }

        public DbSet<WeatherForecastDetail> ForecastDetails { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UnitOfMeasureEfConfig());
        }
    }
}
