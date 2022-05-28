using Core.Models.Common;

namespace Core.Interfaces;

public interface ICacheRepository
{
    Task RemoveLocation(string key, string member);
    Task<bool> SetKey(string key, string value, TimeSpan timeToLive);
    Task<T> GetKey<T>(string key);
    Task<bool> DeleteKey(string key);
    Task<long> AddLocation(string key, string member, ulong lat, ulong lng);
    Task<double> GetDistance(string key, string member1, string member2, string unit = "m");
    Task AddList(string listId, string value);
    Task<long> GetListLen(string listId);
    Task<long> Increase(string key, int value = 1);
    Task Increment(string key, int value = 1, TimeSpan timeToLive = default);
    Task<IEnumerable<string>> GetList(string listName, int start, int length);
    Task<long> RemoveFromList(string listName, string member);
    Task<Position?> GetPosition(string key, string member);
    Task<IEnumerable<string>> GetRadius(string key, ulong lat, ulong lng, int radius);
    Task<IEnumerable<string>?> GetLastPositionOnTask(string listName);
}