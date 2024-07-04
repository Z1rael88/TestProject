using System.Collections;
using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.ProjectRepositories;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectModel>> GetAllAsync(SearchDto searchDto);
    Task<ProjectModel?> GetByIdAsync(Guid id);
    Task<ProjectModel> CreateAsync(ProjectModel entity);
    Task<ProjectModel> UpdateAsync(Guid id, ProjectModel  entity);
    Task DeleteAsync(Guid id);
}