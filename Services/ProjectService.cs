using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProjectService(IMockProjectRepository mockProjectRepository) : IProjectService
{
    public List<ProjectResponse> GetAll()
    {
        var entities = mockProjectRepository.GetAll().Select(p=>p.ProjectToResponse());
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

    public List<ProjectResponse> Search(string searchTerm, string descriptionTerm)
    {
        var entities= mockProjectRepository.Search(searchTerm, descriptionTerm);
        return entities.Select(entity => entity.ProjectToResponse()).ToList();
    }

    public bool Delete(Guid id)
    {
        return mockProjectRepository.Delete(id);
    }
}
    
    