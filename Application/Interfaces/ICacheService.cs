namespace Application.Interfaces;

public interface ICacheService
{
    Task<T?> TryGetCacheDataAsync<T>(object key);
    Task SetCacheDataAsync<T>(object key, T data);
    Task RemoveCacheDataAsync<T>(object key);
}