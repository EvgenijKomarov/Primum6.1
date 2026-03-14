using CoreConnection.DTOs;
using CoreConnection.Entities;
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

        public static async Task<PageResult<TEntity>> ToPageResult<TEntity>(
            this IQueryable<TEntity> queryable, 
            int page, 
            int pageSize,
            CancellationToken cancellationToken = default)
            where TEntity : IHasId
        {
            var totalCount = await queryable.CountAsync(cancellationToken);
            var pageItems = queryable.Skip(page * pageSize).Take(pageSize).OrderBy(x => x.Id);

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
