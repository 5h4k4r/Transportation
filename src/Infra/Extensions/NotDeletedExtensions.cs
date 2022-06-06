using System.Linq.Expressions;

namespace Infra.Extensions;

public static class NotDeletedExtensions
{
    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> query) where T : class
    {
        var parameter = Expression.Parameter(query.ElementType, ""); // x
        var property = Expression.Property(parameter, "DeletedAt"); // x.DeleteAt
        var lambda = Expression.Lambda(property, parameter); // x => x.DeleteAt
        var methodName = "Where";

        Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
            new[] { query.ElementType, property.Type },
            query.Expression, Expression.Quote(lambda));


        return query.Provider.CreateQuery<T>(methodCallExpression);
    }
}