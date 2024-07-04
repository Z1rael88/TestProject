using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public async Task<ICollection<ProjectResponse>> GetAllAsync(SearchDto searchDto)
    {
        var entities = await projectRepository.GetAllAsync(searchDto);
        var responses = entities.Select(p => p.ProjectToResponse());
        if (!string.IsNullOrEmpty(searchDto.SearchTerm))
        {
            responses = responses
                .Where(p => p.Name.Contains(searchDto.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchDto.DescriptionTerm))
        {
            responses = responses
                .Where(p => p.Name.Contains(searchDto.DescriptionTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (responses.Count() == 0)
        {
            throw new NotFoundException("No projects found matching the search criteria.");
        }
        return responses.ToList();
    }
    public async Task<ProjectResponse?> GetByIdAsync(Guid id)
    {
        var entity =  projectRepository.GetByIdAsync(id);
        return await entity?.ProjectToResponseAsync();
    }
    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        var entity = new ProjectModel
        {
            Description = projectRequest.Description,
            Name = projectRequest.Name,
            StartDate = projectRequest.StartDate,
        };
        var createdEntity = projectRepository.CreateAsync(entity);
        return await createdEntity.ProjectToResponseAsync();
    }
    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        var entity= projectRepository.UpdateAsync(id, projectRequest.ProjectRequestToTaskModel());
        return await entity.ProjectToResponseAsync();
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await projectRepository.DeleteAsync(id);
    }
}
    
    