using System.ComponentModel.DataAnnotations;
using Transportation.Api.Model;

namespace Tranportation.Api.Interfaces;

public interface IUserRepository
{

    Task<User?> GetUserById(int Id);
    Task<User?> GetUserByPhone(string Phone);
    Task<User?> GetUserByAuthId(string AuthId);
}