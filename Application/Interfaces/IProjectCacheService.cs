using Application.Dtos.ProjectDtos;

namespace Application.Interfaces;

public interface IProjectCacheService
{
    Task<T> TryGetProjectOrProjects<T>(string cacheKey) where T : class;
    Task<IEnumerable<ProjectResponse>> SetCacheForProjects(string cacheKey,IEnumerable<ProjectResponse> mappedProjects, TimeSpan expiration);
    Task<ProjectResponse> SetCacheForProject(string cacheKey,ProjectResponse mappedProject, TimeSpan expiration);
    Task RemoveCacheFromProject(string cacheKey);
}