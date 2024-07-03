using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IProjectService
{
    public List<ProjectResponse> GetAll();
    public ProjectResponse? GetById(Guid id);
    public ProjectResponse Create(ProjectRequest projectRequest);
    public bool Delete(Guid id);
    public ProjectResponse? Update(Guid id, ProjectRequest projectRequest);
   
}