using Core.Models;
using Core.Requests;

namespace Core.Interfaces;

public interface IAreaInfosRepository
{
    Task<AreaInfoDTO?> GetAreaInfoById(ulong Id);

    Task<AreaInfoDTO?> GetAreaInfoByUser(UserDTO user);
    Task<AreaInfoDTO?> GetAreaInfoByTitle(string Title);
    Task<List<AreaInfoDTO>> ListAreaInfos(ListAreaInfosRequest model);


}