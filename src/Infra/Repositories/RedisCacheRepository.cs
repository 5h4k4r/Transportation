using AutoMapper;
using Core.Interfaces;
using Core.Models.Common;
using Infra.Entities;
using Infra.Interfaces;
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
        await using var client = await _cache.GetClientAsync();
        await client.RemoveItemFromSortedSetAsync(key.Prefixed(), member);
    }

    public async Task<bool> SetKey<T>(string key, T value, TimeSpan timeToLive)
    {
        // TODO: Sliding expire time
        await using var client = await _cache.GetClientAsync();
        return await client.SetAsync<T>(key.Prefixed(), value, timeToLive);
    }

    public async Task<T> GetKey<T>(string key)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.GetAsync<T>(key.Prefixed());
    }

    public async Task<bool> DeleteKey(string key)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.RemoveAsync(key.Prefixed());
    }

    public async Task<long> AddLocation(string key, string member, ulong lat, ulong lng)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.AddGeoMemberAsync(key.Prefixed(), lng, lat, member);
    }

    public async Task<double> GetDistance(string key, string member1, string member2, string unit = RedisGeoUnit.Meters)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.CalculateDistanceBetweenGeoMembersAsync(key.Prefixed(), member1, member2, unit);
    }

    public async Task AddList(string listId, string value)
    {
        await using var client = await _cache.GetClientAsync();

        await client.PushItemToListAsync(listId.Prefixed(), value);
    }

    public async Task<long> GetListLen(string listId)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.GetListCountAsync(listId.Prefixed());
    }

    public async Task<long> Increase(string key, int value = 1)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.IncrementValueByAsync(key.Prefixed(), value);
    }

    public async Task Increment(string key, int value = 1, TimeSpan timeToLive = default)
    {
        await using var client = await _cache.GetClientAsync();

        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetList(string listName, int start, int length)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.GetRangeFromListAsync(listName.Prefixed(), start, length);
    }

    public async Task<long> RemoveFromList(string listName, string member)
    {
        await using var client = await _cache.GetClientAsync();

        return await client.RemoveItemFromListAsync(listName.Prefixed(), member);
    }

    public async Task<Position?> GetPosition(string key, string member)
    {
        await using var client = await _cache.GetClientAsync();

        var pos = await client.GetGeoCoordinatesAsync(key.Prefixed(), member);

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
        await using var client = await _cache.GetClientAsync();

        return await client.FindGeoMembersInRadiusAsync(key.Prefixed(), lat, lng, radius, RedisGeoUnit.Meters);
    }

    public async Task<IEnumerable<string>?> GetLastPositionOnTask(string listName)
    {
        await using var client = await _cache.GetClientAsync();

        var items = await client.GetAllItemsFromListAsync(listName.Prefixed());
        return items?[0].Split('_');
    }
}

internal static class StringExtension
{
    public static string Prefixed(this string staring)
    {
        return $"transportation_{staring}";
    }
}