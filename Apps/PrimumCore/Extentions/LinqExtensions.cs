using CoreDBModel.Extensions;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace PrimumCore.Extentions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition
                ? source.Where(predicate.GetCompiled())
                : source;
        }

        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
