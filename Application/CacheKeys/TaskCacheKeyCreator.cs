using Domain.SearchParams;

namespace Application.CacheKeys;

public static class TaskCacheKeyCreator
{
    public static string GetTaskCacheKey()
    {
        return $"project_Key";
    }
}