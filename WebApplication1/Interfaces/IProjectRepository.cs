using WebApplication1.Dtos.SearchParams;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectModel>> GetAllAsync(ProjectSearchParams projectSearchParams);
    Task<ProjectModel?> GetByIdAsync(Guid id);
    Task<ProjectModel> CreateAsync(ProjectModel entity);
    Task<ProjectModel> UpdateAsync(Guid id, ProjectModel entity);
    Task DeleteAsync(Guid id);
}