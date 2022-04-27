using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeatherForecast.Domain.Persistence;

namespace WeatherForecast.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(WeatherForecastContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetListAsync(List<Expression<Func<T, bool>>>? predicates = null,
            List<Expression<Func<T, object>>>? includes = null, bool isNoTracking = false,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();

            query = AddIncludes(query, includes);
            query = AddPredicates(predicates, query);

            if (isNoTracking)
                query = query.AsNoTracking();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task InsertRangeAsync(List<T> items, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(items, cancellationToken);
        }

        private IQueryable<T> AddIncludes(IQueryable<T> query, List<Expression<Func<T, object>>>? includes = null, bool splitQuery = false)
        {
            if (includes == null) return query;

            foreach (var include in includes)
            {
                if (include.Body is MemberExpression memberExpression)
                {
                    query = splitQuery ? query.Include(include).AsSplitQuery() : query.Include(include);
                }

                if (include.Body.Type == typeof(String))
                    query = splitQuery
                        ? query.Include(include.Body.ToString().Replace("\"", "")).AsSplitQuery()
                        : query.Include(include.Body.ToString().Replace("\"", ""));
            }

            return query;
        }

        private IQueryable<T> AddPredicates(List<Expression<Func<T, bool>>>? predicates, IQueryable<T> query)
        {
            if (predicates != null)
                foreach (var predicate in predicates)
                    query = query.Where(predicate);

            return query;
        }
    }
}
