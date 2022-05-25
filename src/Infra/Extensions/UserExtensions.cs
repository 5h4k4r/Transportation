using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Extensions;

public static class UserExtensions
{
    public static IQueryable<User> WithRoleUser(this IQueryable<User> query, bool withRoleUsers = false)
    {

        if (withRoleUsers)
            return query.Include(x => x.RoleUsers);

        return query;
    }
}