using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services;

public class ProjectCacheService(IMemoryCache memoryCache) : IProjectCacheService
{
    public Task<T> TryGetProjectOrProjects<T>(string cacheKey) where T : class
    {
        if (memoryCache.TryGetValue(cacheKey, out T? cachedProjectResponses) &&
            cachedProjectResponses != null)
        {
            return Task.FromResult(cachedProjectResponses);
        }

        return Task.FromResult<T>(null!);
    }

    public Task<IEnumerable<ProjectResponse>> SetCacheForProjects(string cacheKey,
        IEnumerable<ProjectResponse> mappedProjects, TimeSpan expiration)
    {
        return Task.FromResult(memoryCache.Set(cacheKey, mappedProjects, expiration));
    }

    public Task<ProjectResponse> SetCacheForProject(string cacheKey, ProjectResponse mappedProject, TimeSpan expiration)
    {
        return Task.FromResult(memoryCache.Set(cacheKey, mappedProject, expiration));
    }

    public Task RemoveCacheFromProject(string cacheKey)
    {
        memoryCache.Remove(cacheKey);
        return Task.CompletedTask;
    }
}