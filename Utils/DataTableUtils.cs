using System.Linq.Expressions;
using MVC.Payloads.Requests;

namespace MVC.Utils{
    public static class DataTableUtils{
        public static IOrderedQueryable<T>? Order<T>(IQueryable<T> query, 
        DataTableParameters parameters, params Expression<Func<T, object>>[] mapping) {
            IOrderedQueryable<T>? orderedQuery = null;
            foreach (var order in parameters.order)
            {
                orderedQuery =
                    order.dir == "asc"
                        ? OrderBy(orderedQuery ?? query, mapping[order.column], orderedQuery != null)
                        : OrderBy(orderedQuery ?? query, mapping[order.column], orderedQuery != null, true);
            }
        
            return orderedQuery;
        }
        public static Expression<Func<T, U>>? ConvertExpression<T, U>(Expression<Func<T, object>> expression) {
            if (expression.Body is UnaryExpression unaryExpression)
            {
                var propertyExpression = (MemberExpression)unaryExpression.Operand;
        
                if (propertyExpression.Type == typeof(U))
                    return Expression.Lambda<Func<T, U>>(propertyExpression, expression.Parameters);
            }
        
            return null;
        }
        private static IOrderedQueryable<T> OrderBy<T>(IQueryable<T> qry, 
        Expression<Func<T, object>> expr, bool ordered = false, bool descending = false)
        {
            // Implement conversions as needed
            var t = (expr.Body as UnaryExpression)?.Operand.Type;
        
            if (t == typeof(DateTime))
                return OrderBy<T, DateTime>(qry, expr, ordered, descending);
        
            if (t == typeof(DateTime?))
                return OrderBy<T, DateTime?>(qry, expr, ordered, descending);
        
            return ordered
                ? descending
                    ? (qry as IOrderedQueryable<T>).ThenByDescending(expr)
                    : (qry as IOrderedQueryable<T>).ThenBy(expr)
                : descending
                    ? qry.OrderByDescending(expr)
                    : qry.OrderBy(expr);
        }
        private static IOrderedQueryable<T> OrderBy<T, TU>(IQueryable<T> qry, 
        Expression<Func<T, object>> expr, bool ordered = false, bool descending = false)
        {
            return ordered
                ? descending
                    ? (qry as IOrderedQueryable<T>).ThenByDescending(ConvertExpression<T, TU>(expr))
                    : (qry as IOrderedQueryable<T>).ThenBy(ConvertExpression<T, TU>(expr))
                : descending
                    ? qry.OrderByDescending(ConvertExpression<T, TU>(expr))
                    : qry.OrderBy(ConvertExpression<T, TU>(expr));
        }
    }
}