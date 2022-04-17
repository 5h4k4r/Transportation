using System.ComponentModel.DataAnnotations;
using Transportation.Api.Model;

namespace Tranportation.Api.Interfaces;

public interface IUserRepository
{

    Task<User?> GetUserById([Required] int Id);
    Task<User?> GetUserByPhone([Required] string Phone);
    Task<User?> GetUserByAuthId([Required] string AuthId);
}