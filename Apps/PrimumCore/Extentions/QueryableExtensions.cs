using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using System.Linq.Expressions;

namespace PrimumCore.Extentions
{
    public static class QueryableExtensions
    {
        public static async Task<TEntity> One<TEntity>(
            this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var entity = predicate == null
                ? await queryable.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false)
                : await queryable.FirstOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);

            return entity ?? throw new NotFoundException(typeof(TEntity).Name);
        }

        public static async Task<IEnumerable<TEntity>> Many<TEntity>(
            this IQueryable<TEntity> queryable,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var entities = await queryable.ToArrayAsync();
            return entities;
        }
    }
}
