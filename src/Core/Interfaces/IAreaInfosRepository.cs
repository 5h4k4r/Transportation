using Core.Models.Base;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IAreaInfosRepository
{
    Task<AreaInfoDto?> GetAreaInfoById(ulong id);

    Task<AreaInfoDto?> GetAreaInfoByUser(UserDto user);
    Task<AreaInfoDto?> GetAreaInfoByTitle(string title);
    Task<List<AreaInfoDto>> ListAreaInfos(ListAreaInfosRequest model);


}