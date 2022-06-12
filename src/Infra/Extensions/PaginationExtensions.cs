using System.Linq.Expressions;
using Core;
using Core.Constants;
using Core.Interfaces;

namespace Infra.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> dbSet, IPagingOptions model) where T : class
    {
        var limit = model.Limit ?? CoreConstants.DefaultPaginationLimit;
        var page = model.Page ?? 0;


        return dbSet.Skip(page * limit).Take(limit);
    }

    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, ISortOptions model)
    {
        try
        {
            // return unsorted if no property name was given
            if (string.IsNullOrEmpty(model.SortField))
                return source;
            // Sorted by property of a property ie. Task.Id
            if (model.SortField.Contains('.'))
            {
                var nameParts = model.SortField.Split('.');
                var parentPropertyName = nameParts[0];
                var childPropertyName = nameParts[1];

                var parameterExpression = Expression.Parameter(source.ElementType, "x"); // x

                var parentProperty = Expression.Property(parameterExpression, parentPropertyName); // x.Task
                var childProperty = Expression.Property(parentProperty, childPropertyName); // x.Task.id

                var lambdaExpression = Expression.Lambda(childProperty, parameterExpression); // param_0 => x.Task.id
                var methodName = model.SortDescending ?? false ? "OrderByDescending" : "OrderBy";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                    new[] { source.ElementType, childProperty.Type },
                    source.Expression, Expression.Quote(lambdaExpression));

                return source.Provider.CreateQuery<T>(methodCallExpression);
            }
            else // Sorted by a property ie. Id
            {
                var parameter = Expression.Parameter(source.ElementType, ""); // x
                var property = Expression.Property(parameter, model.SortField); // x.id
                var lambda = Expression.Lambda(property, parameter); // x => x.id

                var methodName = model.SortDescending ?? false ? "OrderByDescending" : "OrderBy";

                Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                    new[] { source.ElementType, property.Type },
                    source.Expression, Expression.Quote(lambda));

                return source.Provider.CreateQuery<T>(methodCallExpression);
            }
        }
        catch
        {
            return source;
        }
    }
}