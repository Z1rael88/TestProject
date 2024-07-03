using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IMockProjectRepository
{
    List<ProjectModel> GetAll();
    ProjectModel? GetById(Guid id);
    ProjectModel Create(ProjectModel entity);
    ProjectModel? Update(Guid id, ProjectModel  entity);
    
    bool Delete(Guid id);
}