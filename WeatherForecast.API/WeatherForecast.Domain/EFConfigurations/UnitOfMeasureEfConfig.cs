using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain.EFConfigurations
{
    public class UnitOfMeasureEfConfig : IEntityTypeConfiguration<UnitOfMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "ft", 0));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "in", 1));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "mi", 2));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "mm", 3));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "cm", 4));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "m", 5));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "km", 6));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "km/h", 7));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "kt", 8));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "mi/h", 9));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "m/s", 10));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "hPa", 11));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "Hg", 12));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "kPa", 13));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "mbar", 14));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "mmHg", 15));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "psi", 16));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "C", 17));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "F", 18));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "K", 19));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "%", 20));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "f", 21));
            builder.HasData(new UnitOfMeasure(Guid.NewGuid(), "int", 22));
        }
    }
}
