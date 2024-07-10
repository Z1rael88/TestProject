using Domain.SearchParams;

namespace Application.CacheKeys;

public static class ProjectCacheKeyCreator
{
    public static string GetCacheKeyForProject(Guid id)
    {
        return $"project_{id.GetHashCode()}";
    }
    public static string GetCacheKeyForAllProjects(ProjectSearchParams projectSearchParams)
    {
        return $"projects_{projectSearchParams.GetHashCode()}";
    }
}