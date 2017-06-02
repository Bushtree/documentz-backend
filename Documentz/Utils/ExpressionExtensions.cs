using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Documentz.Utils
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<TConcreteInput, TOutput>> ToConcreteType<TInput, TConcreteInput, TOutput>(this Expression<Func<TInput, TOutput>> expression)
        {
            var memberName = ((MemberExpression)expression.Body).Member.Name;

            var param = Expression.Parameter(typeof(TInput));
            var field = Expression.Property(param, memberName);
            return Expression.Lambda<Func<TConcreteInput, TOutput>>(field, param);
        }
    }
}
