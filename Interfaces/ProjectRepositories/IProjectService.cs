using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Dtos.SearchParams;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectService
{
    public Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams);
    public Task<ProjectResponse> GetByIdAsync(Guid id);
    public Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest);
    public Task DeleteAsync(Guid id);
    public Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest);
}