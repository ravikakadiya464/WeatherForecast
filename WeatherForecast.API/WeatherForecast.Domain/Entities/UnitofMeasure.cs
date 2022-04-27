using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Domain.Entities
{
    public class UnitOfMeasure
    {
        public Guid Id { get; protected set; }

        [Required]
        public string Unit { get; protected set; }

        [Required]
        public int UnitType { get; protected set; }

        protected UnitOfMeasure()
        {

        }

        public UnitOfMeasure(Guid id, string unit, int unitType)
        {
            Id = id;
            Unit = unit;
            UnitType = unitType;
        }
    }
}
