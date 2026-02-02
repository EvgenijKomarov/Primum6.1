using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace PrimumCore.Extentions
{
    public static class ExpressionToFuncExtensions
    {
        private static readonly ConcurrentDictionary<Expression, Delegate> _compiledCache =
            new ConcurrentDictionary<Expression, Delegate>();

        /*public static bool Evaluate<T>(this Expression<Func<T, bool>> expression, T args)
        {
            var func = expression.GetCompiled();
            return func(args);
        }*/

        public static Func<T, bool> GetCompiled<T>(this Expression<Func<T, bool>> expression)
        {
            return (Func<T, bool>)_compiledCache.GetOrAdd(
                expression,
                _ => expression.Compile()
            );
        }
    }
}
