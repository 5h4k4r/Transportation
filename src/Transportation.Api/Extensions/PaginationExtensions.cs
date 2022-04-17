using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tranportation.Api;
using Transportation.Api.Common;
using Transportation.Api.Interfaces;

namespace Transportation.Api.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> dbSet, IPagingOptions model) where T : class
    {

        int Limit = model.Limit ?? Constants.DefaultPaginationLimit;
        int Page = model.Page ?? 0;


        return dbSet.Skip(Page * Limit).Take(Limit);
    }

    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, string? columnName, bool isDescending = true)
    {
        try
        {
            if (string.IsNullOrEmpty(columnName))
                return source;


            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isDescending ? "OrderByDescending" : "OrderBy";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
        catch
        {
            return source;
        }
    }
}