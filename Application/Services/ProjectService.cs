using Application.CacheKeys;
using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    IValidator<ProjectRequest> projectValidator,
    IMapper mapper,
    ILogger<ProjectService> logger,
    IMemoryCache memoryCache)
    : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        var cacheKey = ProjectCacheKeyCreator.GetProjectCacheKey();
        logger.LogInformation("Started retrieving projects from Service Layer");
        if (memoryCache.TryGetValue(cacheKey, out IEnumerable<ProjectResponse>? cachedProjectResponses) &&
            cachedProjectResponses != null)
        {
            logger.LogInformation("Successfully retrieved projects from cache from Service Layer");
            return cachedProjectResponses;
        }

        var projects = await projectRepository.GetAllAsync(projectSearchParams);
        var mappedProjects = mapper.Map<IEnumerable<ProjectResponse>>(projects);
        var projectResponses = memoryCache.Set(cacheKey, mappedProjects, TimeSpan.FromMinutes(2));
        logger.LogInformation("Successfully retrieved projects from Service Layer");
        return projectResponses;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        var cacheKey = ProjectCacheKeyCreator.GetProjectCacheKey();
        logger.LogInformation($"Started retrieving project with Id : {id} from Service Layer");
        if (memoryCache.TryGetValue(cacheKey, out ProjectResponse? cachedProjectResponse) &&
            cachedProjectResponse != null)
        {
            logger.LogInformation("Successfully retrieved projects from cache from Service Layer");
            return cachedProjectResponse;
        }

        var project = await projectRepository.GetByIdAsync(id);
        var mappedProject = mapper.Map<ProjectResponse>(project);
        var projectResponse = memoryCache.Set(cacheKey, mappedProject, TimeSpan.FromMinutes(2));
        logger.LogInformation($"Successfully retrieved project with Id : {project.Id} from Service Layer");
        return projectResponse;
    }

    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        logger.LogInformation("Started creating project from Service Layer");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = mapper.Map<ProjectModel>(projectRequest);
        var createdProject = await projectRepository.CreateAsync(project);
        var projectResponse = mapper.Map<ProjectResponse>(createdProject);
        logger.LogInformation(
            $"Successfully created project with Id : {createdProject.Id} from Service Layer");
        return projectResponse;
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        logger.LogInformation($"Started updating project with Id : {id} from Service Layer");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = await projectRepository.GetByIdAsync(id);
        mapper.Map(projectRequest, project);
        var updatedProject = await projectRepository.UpdateAsync(project);
        var projectResponse = mapper.Map<ProjectResponse>(updatedProject);
        var cacheKey = TaskCacheKeyCreator.GetTaskCacheKey();
        memoryCache.Set(cacheKey, projectResponse, TimeSpan.FromMinutes(2));
        logger.LogInformation(
            $"Successfully updated project with Id : {updatedProject.Id} from Service Layer");
        return projectResponse;
    }

    public async Task DeleteAsync(Guid id)
    {
        var cacheKey = ProjectCacheKeyCreator.GetProjectCacheKey();
        logger.LogInformation($"Started deleting project with Id : {id} from Service Layer");
        await projectRepository.DeleteAsync(id);
        memoryCache.Remove(cacheKey);
        logger.LogInformation($"Successfully deleted project with Id : {id} from Service Layer");
    }
}