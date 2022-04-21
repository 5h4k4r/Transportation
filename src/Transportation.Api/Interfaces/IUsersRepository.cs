using System.ComponentModel.DataAnnotations;
using Transportation.Api.Model;

namespace Tranportation.Api.Interfaces;

public interface IUsersRepository
{

    Task<User?> GetUserById(int Id);
    Task<User?> GetUserByPhone(string Phone, bool withRoleUsers = false);
    Task<User?> GetUserByAuthId(string AuthId, bool withRoleUsers = false);
}