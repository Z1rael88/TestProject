using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    IValidator<ProjectRequest> projectValidator,
    IMapper mapper,
    ILogger<ProjectService> logger)
    : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        var projects = await projectRepository.GetAllAsync(projectSearchParams);
        var responses = mapper.Map<IEnumerable<ProjectResponse>>(projects);
        logger.LogInformation("Successfully retrieved projects");
        return responses;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        var response = mapper.Map<ProjectResponse>(project);
        logger.LogInformation($"Successfully retrieved project with id : {project.Id}");
        return response;
    }

    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        logger.LogInformation($"Started creating project with Name : {projectRequest.Name}");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = mapper.Map<ProjectModel>(projectRequest);
        var createdProject = await projectRepository.CreateAsync(project);
        var response = mapper.Map<ProjectResponse>(createdProject);
        logger.LogInformation(
            $"Successfully created project with Name : {createdProject.Name} and Id : {createdProject.Id}");
        return response;
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        logger.LogInformation($"Started updating project with Name : {projectRequest.Name}");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = await projectRepository.GetByIdAsync(id);
        mapper.Map(projectRequest, project);
        var updatedProject = await projectRepository.UpdateAsync(project);
        var response = mapper.Map<ProjectResponse>(updatedProject);
        logger.LogInformation(
            $"Successfully updated project with Name : {updatedProject.Name} and Id : {updatedProject.Id}");
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        await projectRepository.DeleteAsync(id);
        logger.LogInformation($"Successfully deleted project with id : {id}");
    }
}