using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Dtos.SearchParams;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        var entities = await projectRepository.GetAllAsync(projectSearchParams);
        var responses = entities.Select(p => p.ProjectToResponse());
        return responses;
    }
    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        var entity = await projectRepository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException();
        return entity.ProjectToResponse();
    }
    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        var entity = new ProjectModel
        {
            Description = projectRequest.Description,
            Name = projectRequest.Name,
            StartDate = projectRequest.StartDate,
        };
        var createdEntity = await projectRepository.CreateAsync(entity);
        return createdEntity.ProjectToResponse();
    }
    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        var entity = await projectRepository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException();
        entity.Name = projectRequest.Name;
        entity.Description = projectRequest.Description;
        var response= await projectRepository.UpdateAsync(id, projectRequest.ProjectRequestToTaskModel());
        return response.ProjectToResponse();
    }
    public async Task DeleteAsync(Guid id)
    {
         await projectRepository.DeleteAsync(id);
    }
}
    
    