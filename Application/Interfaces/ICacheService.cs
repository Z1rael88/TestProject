namespace Application.Interfaces;

public interface ICacheService
{
    Task<T?> TryGetCacheData<T>(object key);
    Task SetCacheData<T>(object key, T data);
    Task RemoveCacheData<T>(object key);
}