using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectService
{
    public ICollection<ProjectResponse> GetAll(SearchDto searchDto);
    public ProjectResponse? GetById(Guid id);
    public ProjectResponse Create(ProjectRequest projectRequest);
    public bool Delete(Guid id);
    public ProjectResponse? Update(Guid id, ProjectRequest projectRequest);
}