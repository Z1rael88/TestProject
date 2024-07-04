using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectService
{
    public Task<ICollection<ProjectResponse>> GetAllAsync(SearchDto searchDto);
    public Task<ProjectResponse?> GetByIdAsync(Guid id);
    public Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest);
    public Task DeleteAsync(Guid id);
    public Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest);
}