using Domain.Models;
using Domain.SearchParams;
using WebApplication1.Models;

namespace Domain.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectModel>> GetAllAsync(ProjectSearchParams projectSearchParams);
    Task<ProjectModel> GetByIdAsync(Guid id);
    Task<ProjectModel> CreateAsync(ProjectModel project);
    Task<ProjectModel> UpdateAsync(Guid id, ProjectModel project);
    Task DeleteAsync(Guid id);
}