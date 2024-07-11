using System.Text;
using System.Text.Json;
using Application.CacheKeys;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;


namespace Application.Services;

public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    public async Task<T?> TryGetCacheDataAsync<T>(object key)
    {
        var cacheKey = CacheKeyGenerator.GenerateCacheKey(nameof(T), key);
        var cachedData = await distributedCache.GetAsync(cacheKey);
        return cachedData == null ? default : JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task SetCacheDataAsync<T>(object key, T data)
    {
        var cacheKey = CacheKeyGenerator.GenerateCacheKey(nameof(T), key);
        var cachedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        await distributedCache.SetAsync(cacheKey, cachedData);
    }

    public async Task RemoveCacheDataAsync<T>(object key)
    {
        var cacheKey = CacheKeyGenerator.GenerateCacheKey(nameof(T), key);
        await distributedCache.RemoveAsync(cacheKey);
    }
}