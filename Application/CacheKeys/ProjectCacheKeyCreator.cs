using Domain.SearchParams;

namespace Application.CacheKeys;

public static class ProjectCacheKeyCreator
{
    public static string GetProjectCacheKey()
    {
        return $"project_Key";
    }
}