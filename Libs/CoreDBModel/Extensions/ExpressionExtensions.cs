using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace CoreDBModel.Extensions
{
    public static class ExpressionExtensions
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

        public static Expression<Func<TSource, bool>> AndWithProperty<TSource, TProperty>(
            this Expression<Func<TSource, bool>> sourceCondition,
            Expression<Func<TSource, TProperty>> propertySelector,
            Expression<Func<TProperty, bool>> propertyCondition) where TProperty : class
        {
            // 1. Заменяем параметр propertySelector на параметр sourceCondition
            var selectorReplacer = new ParameterReplacer(
                propertySelector.Parameters[0],
                sourceCondition.Parameters[0]);
            var normalizedPropertyAccess = selectorReplacer.Visit(propertySelector.Body);

            // 2. Заменяем параметр propertyCondition на нормализованный доступ к свойству
            var conditionReplacer = new ParameterReplacer(
                propertyCondition.Parameters[0],
                normalizedPropertyAccess);
            var replacedConditionBody = conditionReplacer.Visit(propertyCondition.Body);

            // 3. Проверка на null: проверяем владельца свойства (если возможно), чтобы избежать обращения к null
            Expression ownerExpression = null;
            if (propertySelector.Body is MemberExpression memberExpr && memberExpr.Expression != null)
            {
                // заменить параметр владельца на параметр источника
                ownerExpression = selectorReplacer.Visit(memberExpr.Expression);
            }

            Expression nullCheck;
            if (ownerExpression != null)
            {
                nullCheck = Expression.NotEqual(
                    ownerExpression,
                    Expression.Constant(null, ownerExpression.Type));
            }
            else
            {
                // fallback: проверяем саму нормализованную property access (могут быть случаи, где Body не MemberExpression)
                nullCheck = Expression.NotEqual(
                    normalizedPropertyAccess,
                    Expression.Constant(null, typeof(TProperty)));
            }

            // 4. Объединяем: (owner != null) AND (условие_свойства)
            var propertyCheck = Expression.AndAlso(nullCheck, replacedConditionBody);

            // 5. Объединяем с основным условием
            var combinedBody = Expression.AndAlso(sourceCondition.Body, propertyCheck);

            return Expression.Lambda<Func<TSource, bool>>(combinedBody, sourceCondition.Parameters[0]);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _from;
            private readonly Expression _to;

            public ParameterReplacer(ParameterExpression from, Expression to)
            {
                _from = from;
                _to = to;
            }

            protected override Expression VisitParameter(ParameterExpression node)
                => node == _from ? _to : base.VisitParameter(node);
        }
    }
}
