using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Repository.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Domain.Entities.WeatherForecast> WeatherForecastRepository { get; }
        IGenericRepository<UnitOfMeasure> UnitOfMeasureRepository { get; }
        Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
