using AutoMapper;
using Core.Interfaces;
using Core.Models.Common;
using Infra.Entities;
using ServiceStack.Redis;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class RedisCacheRepository : ICacheRepository
{
    private readonly IRedisClientsManagerAsync _cache;
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public RedisCacheRepository(TransportationContext context, IMapper mapper, IRedisClientsManagerAsync cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }


    public async Task RemoveLocation(string key, string member)
    {
        var client = await _cache.GetClientAsync();
        await client.RemoveItemFromSortedSetAsync(key, member);
    }

    public async Task<bool> SetKey(string key, string value, TimeSpan timeToLive)
    {
        // TODO: Sliding expire time
        var client = await _cache.GetClientAsync();
        return await client.SetAsync(key, value, timeToLive);
    }

    public async Task<T> GetKey<T>(string key)
    {
        var client = await _cache.GetClientAsync();

        return await client.GetAsync<T>(key);
    }

    public async Task<bool> DeleteKey(string key)
    {
        var client = await _cache.GetClientAsync();

        return await client.RemoveAsync(key);
    }

    public async Task<long> AddLocation(string key, string member, ulong lat, ulong lng)
    {
        var client = await _cache.GetClientAsync();

        return await client.AddGeoMemberAsync(key, lng, lat, member);
    }

    public async Task<double> GetDistance(string key, string member1, string member2, string unit = RedisGeoUnit.Meters)
    {
        var client = await _cache.GetClientAsync();

        return await client.CalculateDistanceBetweenGeoMembersAsync(key, member1, member2, unit);
    }

    public async Task AddList(string listId, string value)
    {
        var client = await _cache.GetClientAsync();

        await client.PushItemToListAsync(listId, value);
    }

    public async Task<long> GetListLen(string listId)
    {
        var client = await _cache.GetClientAsync();

        return await client.GetListCountAsync(listId);
    }

    public async Task<long> Increase(string key, int value = 1)
    {
        var client = await _cache.GetClientAsync();

        return await client.IncrementValueByAsync(key, value);
    }

    public async Task Increment(string key, int value = 1, TimeSpan timeToLive = default)
    {
        var client = await _cache.GetClientAsync();

        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetList(string listName, int start, int length)
    {
        var client = await _cache.GetClientAsync();

        return await client.GetRangeFromListAsync(listName, start, length);
    }

    public async Task<long> RemoveFromList(string listName, string member)
    {
        var client = await _cache.GetClientAsync();

        return await client.RemoveItemFromListAsync(listName, member);
    }

    public async Task<Position?> GetPosition(string key, string member)
    {
        var client = await _cache.GetClientAsync();

        var pos = await client.GetGeoCoordinatesAsync(key, member);

        if (pos == null)
            return null;
        return new Position
        {
            Latitude = pos[0].Latitude,
            Longitude = pos[0].Longitude
        };
    }

    public async Task<IEnumerable<string>> GetRadius(string key, ulong lat, ulong lng, int radius)
    {
        var client = await _cache.GetClientAsync();

        return await client.FindGeoMembersInRadiusAsync(key, lat, lng, radius, RedisGeoUnit.Meters);
    }

    public async Task<IEnumerable<string>?> GetLastPositionOnTask(string listName)
    {
        var client = await _cache.GetClientAsync();

        var items = await client.GetAllItemsFromListAsync(listName);
        return items?[0].Split('_');
    }
}