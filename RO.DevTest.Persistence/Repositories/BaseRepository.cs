using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Interfaces;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Persistence.Repositories
{
    public class BaseRepository<T>(DefaultContext defaultContext) : IBaseRepository<T> where T : class
    {
        private readonly DefaultContext _defaultContext = defaultContext;

        protected DefaultContext Context => _defaultContext;

        // Implementação de IBaseRepository<T>
        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        // Métodos adicionais
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<T>().AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public T? Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return GetQueryWithIncludes(predicate, includes).FirstOrDefault();
        }

        private IQueryable<T> GetQueryWithIncludes(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> baseQuery = GetWhereQuery(predicate);

            foreach (Expression<Func<T, object>> include in includes)
            {
                baseQuery = baseQuery.Include(include);
            }

            return baseQuery;
        }

        private IQueryable<T> GetWhereQuery(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> baseQuery = Context.Set<T>();

            if (predicate is not null)
            {
                baseQuery = baseQuery.Where(predicate);
            }

            return baseQuery;
        }
    }
}