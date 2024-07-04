using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProjectService(IMockProjectRepository mockProjectRepository) : IProjectService
{
    public ICollection<ProjectResponse> GetAll(SearchDto searchDto)
    {
        var entities = mockProjectRepository.GetAll().Select(p => p.ProjectToResponse());
        if (entities == null) return null;
        if (!string.IsNullOrEmpty(searchDto.SearchTerm))
        {
            entities = entities
                .Where(p => p.Name.Contains(searchDto.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchDto.DescriptionTerm))
        {
            entities = entities
                .Where(p => p.Name.Contains(searchDto.DescriptionTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (entities.Count() == 0)
        {
            throw new NotFoundException("No projects found matching the search criteria.");
        }
        return entities.ToList();
    }
    public ProjectResponse? GetById(Guid id)
    {
        var entity =  mockProjectRepository.GetById(id);
        return entity?.ProjectToResponse();
    }
    public ProjectResponse Create(ProjectRequest projectRequest)
    {
        var entity = new ProjectModel
        {
            Description = projectRequest.Description,
            Name = projectRequest.Name,
            StartDate = projectRequest.StartDate,
        };
        var createdEntity = mockProjectRepository.Create(entity);
        return createdEntity.ProjectToResponse();
    }
    public ProjectResponse? Update(Guid id, ProjectRequest projectRequest)
    {
        var entity= mockProjectRepository.Update(id, projectRequest.ProjectRequestToTaskModel());
        return entity?.ProjectToResponse();
    }
    public bool Delete(Guid id)
    {
        return mockProjectRepository.Delete(id);
    }
}
    
    