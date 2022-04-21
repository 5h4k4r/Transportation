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
            if (columnName.Contains('.'))
            {
                var prop = columnName.Split('.');
                var parent = prop[0];
                var child = prop[1];
                ParameterExpression param1 = Expression.Parameter(source.ElementType, "x"); // x
                MemberExpression prop1 = Expression.Property(param1, parent); // x.Task

                ParameterExpression param2 = Expression.Parameter(Type.GetType("Transportation.Api.Model." + parent)!, ""); // Task
                MemberExpression prop2 = Expression.Property(prop1, child); // x.Task.id

                var lambdaaa = Expression.Lambda(prop2, param1); // param_0 => x.Task.id
                string methodNamea = isDescending ? "OrderByDescending" : "OrderBy";

                Expression methodCallExpressiona = Expression.Call(typeof(Queryable), methodNamea,
                                      new Type[] { source.ElementType, prop2.Type },
                                      source.Expression, Expression.Quote(lambdaaa));

                return source.Provider.CreateQuery<T>(methodCallExpressiona);
            }


            ParameterExpression parameter = Expression.Parameter(source.ElementType, ""); // x
            MemberExpression property = Expression.Property(parameter, columnName); // x.id
            LambdaExpression lambda = Expression.Lambda(property, parameter); // x => x.id

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