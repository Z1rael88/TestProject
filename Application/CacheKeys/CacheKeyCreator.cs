namespace Application.CacheKeys;

public static class CacheKeyGenerator
{
    public static string GenerateCacheKey(string name, object key)
    {
        return $"{name}_{key.GetHashCode()}";
    }
}