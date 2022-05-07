using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Core.Interfaces;

public interface IUsersRepository
{

    Task<UserDTO?> GetUserById(int Id);
    Task<UserDTO?> GetUserByPhone(string Phone, bool withRoleUsers = false);
    Task<UserDTO?> GetUserByAuthId(string AuthId, bool withRoleUsers = false);
}