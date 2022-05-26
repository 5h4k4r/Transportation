using AutoMapper;
using Core.Interfaces;
using Infra.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class RedisCacheRepository : ICacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public RedisCacheRepository(TransportationContext context, IMapper mapper, IDistributedCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }


    public Task RemoveLocation(string key, string member)
    {
        throw new NotImplementedException();
    }

    public Task SetKey(string key, string value, TimeSpan timeToLive)
    {
        // TODO: Sliding expire time
        return _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeToLive
        });
    }

    public Task<T> GetKey<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task<T> DeleteKey<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task AddLocation(string key, string member, ulong lat, ulong lng)
    {
        throw new NotImplementedException();
    }

    public Task<double> GetDistance(string key, string member1, string member2, string unit = "m")
    {
        throw new NotImplementedException();
    }

    public Task AddList(string listName, string value)
    {
        throw new NotImplementedException();
    }

    public Task GetListLen(string listName)
    {
        throw new NotImplementedException();
    }

    public Task Increase(string key, int value = 1)
    {
        throw new NotImplementedException();
    }

    public Task Increment(string key, int value = 1, TimeSpan timeToLive = default)
    {
        throw new NotImplementedException();
    }

    public Task GetList(string listName, int start, int length)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromList(string listName, string member)
    {
        throw new NotImplementedException();
    }

    public Task GetPosition(string key, string member)
    {
        throw new NotImplementedException();
    }

    public Task GetRadius(string key, ulong lat, ulong lng, int radius)
    {
        throw new NotImplementedException();
    }

    public Task GetLastPositionOnTask(string listName)
    {
        throw new NotImplementedException();
    }
}