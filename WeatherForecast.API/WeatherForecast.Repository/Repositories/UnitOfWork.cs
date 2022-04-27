using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Persistence;

namespace WeatherForecast.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WeatherForecastContext _context;
        public UnitOfWork(WeatherForecastContext context)
        {
            _context = context;
        }

        private IGenericRepository<Domain.Entities.WeatherForecast> _weatherForecastRepository;
        private IGenericRepository<UnitOfMeasure> _unitofMeasureRepository;

        public IGenericRepository<Domain.Entities.WeatherForecast> WeatherForecastRepository
        {
            get
            {
                if (_weatherForecastRepository != null) return _weatherForecastRepository;

                _weatherForecastRepository = new GenericRepository<Domain.Entities.WeatherForecast>(_context);
                return _weatherForecastRepository;

            }
        }

        public IGenericRepository<UnitOfMeasure> UnitOfMeasureRepository
        {
            get
            {
                if (_unitofMeasureRepository != null) return _unitofMeasureRepository;

                _unitofMeasureRepository = new GenericRepository<UnitOfMeasure>(_context);
                return _unitofMeasureRepository;

            }
        }


        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
