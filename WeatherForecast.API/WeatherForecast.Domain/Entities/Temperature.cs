using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Domain.Entities
{
    public class Temperature
    {
        public Guid Id { get; protected set; }

        public decimal MinValue { get; protected set; }

        public decimal MaxValue { get; protected set; }

        public string? MinPhrase { get; protected set; }

        public string? MaxPhrase { get; protected set; }

        public Guid UnitMeasureId { get; protected set; }

        [ForeignKey(nameof(UnitMeasureId))]
        public virtual UnitOfMeasure UnitMeasure { get; protected set; }

        protected Temperature()
        {

        }

        public Temperature(Guid id, decimal minValue, decimal maxValue, Guid unitMeasureId, string minPhrase = null, string maxPhrase = null)
        {
            Id = id;
            MinValue = minValue;
            MaxValue = maxValue;
            MinPhrase = minPhrase;
            MaxPhrase = maxPhrase;
            UnitMeasureId = unitMeasureId;
        }
    }
}
