using Transportation.Api.Model;

namespace Transportation.Api.Interfaces;

public interface IAreaInfosRepository
{
    Task<AreaInfo?> GetAreaInfoById(ulong Id);
    Task<AreaInfo?> GetAreaInfoByTitle(string Title);
}