using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectService
{
    public Task<ICollection<ProjectResponse>> GetAllAsync();
    public Task<ProjectResponse?> GetByIdAsync(Guid id);
    public Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest);
    public Task<bool> DeleteAsync(Guid id);
    public Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest);
   
}