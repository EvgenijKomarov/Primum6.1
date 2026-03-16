using PrimumCore.Entities;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using System.Linq.Expressions;
using CoreConnection.DTOs.Abstractions;

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

        public static async Task<PageResult<TEntity>> ToPageResult<TEntity>(
            this IQueryable<TEntity> queryable, 
            int page, 
            int pageSize,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var totalCount = await queryable.CountAsync(cancellationToken);

            if (queryable is IQueryable<IOrderable>)//combined sort
            {
                IOrderedQueryable<TEntity> orderedQuery = null;

                if (typeof(IHasRating).IsAssignableFrom(typeof(TEntity)))
                {
                    orderedQuery = orderedQuery is null ?
                        queryable.OrderByDescending(x => ((IHasRating)(object)x).Rating) :
                        orderedQuery.ThenByDescending(x => ((IHasRating)(object)x).Rating);
                }

                if (typeof(IHasDateTime).IsAssignableFrom(typeof(TEntity)))
                {
                    orderedQuery = orderedQuery is null ?
                        queryable.OrderByDescending(x => ((IHasDateTime)(object)x).CreatedAt) :
                        orderedQuery.ThenByDescending(x => ((IHasDateTime)(object)x).CreatedAt);
                }

                if (typeof(IHasId).IsAssignableFrom(typeof(TEntity)))
                {
                    orderedQuery = orderedQuery is null ? 
                        queryable.OrderByDescending(x => ((IHasId)(object)x).Id) : 
                        orderedQuery.ThenByDescending(x => ((IHasId)(object)x).Id);
                }
                if (orderedQuery is not null) queryable = orderedQuery;
            }

            var pageItems = queryable.Skip(page * pageSize).Take(pageSize);

            return new PageResult<TEntity>
            { 
                Items = await pageItems.ToArrayAsync(cancellationToken),
                TotalItemsCount = totalCount,
                TotalPages = totalCount == 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize),
                CurrentPage = page
            };
        }
    }
}
