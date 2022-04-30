using Core.Models;

namespace Core.Interfaces;

public interface IAreaInfosRepository
{
    Task<List<AreaInfoDTO>> GetAreaInfoById(ulong Id);
    Task<List<AreaInfoDTO>> GetAreaInfoByTitle(string Title);
    Task<List<AreaInfoDTO>> ListAreaInfos();
}