namespace Core.Interfaces;

public interface ICacheRepository
{
    Task RemoveLocation(string key, string member);
    Task SetKey(string key, string value, TimeSpan timeToLive);
    Task<T> GetKey<T>(string key);
    Task<T> DeleteKey<T>(string key);
    Task AddLocation(string key, string member, ulong lat, ulong lng);
    Task<double> GetDistance(string key, string member1, string member2, string unit = "m");
    Task AddList(string listName, string value);
    Task GetListLen(string listName);
    Task Increase(string key, int value = 1);
    Task Increment(string key, int value = 1, TimeSpan timeToLive = default);
    Task GetList(string listName, int start, int length);
    Task RemoveFromList(string listName, string member);
    Task GetPosition(string key, string member);
    Task GetRadius(string key, ulong lat, ulong lng, int radius);
    Task GetLastPositionOnTask(string listName);
}