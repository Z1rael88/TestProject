using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectService
{
    public List<ProjectResponse> GetAll();
    public ProjectResponse? GetById(Guid id);
    public ProjectResponse Create(ProjectRequest projectRequest);
    public bool Delete(Guid id);
    public ProjectResponse? Update(Guid id, ProjectRequest projectRequest);
    List<ProjectResponse> Search(string searchTerm, string descriptionTerm);

}