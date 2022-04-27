using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Repository.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetListAsync(List<Expression<Func<T, bool>>> predicates = null,
            List<Expression<Func<T, object>>> includes = null, bool isNoTracking = false,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, CancellationToken cancellationToken = default);

        Task InsertRangeAsync(List<T> items, CancellationToken cancellationToken = default);
    }
}
