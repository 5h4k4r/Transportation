using Transportation.Api.Model;

namespace Transportation.Api.Interfaces;

public interface IAreaInfosRepository
{
    Task<List<AreaInfo>> GetAreaInfoById(ulong Id);
    Task<List<AreaInfo>> GetAreaInfoByTitle(string Title);
    Task<List<AreaInfo>> ListAreaInfos();
}