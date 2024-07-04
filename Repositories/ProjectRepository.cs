using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ProjectRepository() : IProjectRepository
{
    private static List<ProjectModel> _projects =
    [
        new ProjectModel { Id = Guid.NewGuid(), Name = "Project 1", Description = "Description 1", StartDate = DateTime.Now, Tasks = [] },
        new ProjectModel { Id = Guid.NewGuid(), Name = "Project 2", Description = "Description 2", StartDate = DateTime.Now, Tasks = [] }
    ];

    public async Task<IEnumerable<ProjectModel>> GetAllAsync(SearchDto searchDto)
    {
        var allProjects = await Task.Run(() => _projects.ToList());
        if (!string.IsNullOrEmpty(searchDto.SearchTerm))
        {
            allProjects = allProjects
                .Where(p => p.Name.Contains(searchDto.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchDto.DescriptionTerm))
        {
            allProjects = allProjects
                .Where(p => p.Name.Contains(searchDto.DescriptionTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (allProjects.Count == 0)
        {
            throw new NotFoundException("No projects found matching the search criteria.");
        }
        return allProjects;
    }

    public async Task<ProjectModel?> GetByIdAsync(Guid id)
    {
        var project = await Task.Run(()=>_projects.FirstOrDefault(p => p.Id == id));
        return project ?? null; ///not null, there is no project
    }

    public async Task<ProjectModel> CreateAsync(ProjectModel entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _projects.Add(entity);
        return entity;
    }

    public async Task<ProjectModel> UpdateAsync(Guid id, ProjectModel entity)
    {
        var existingEntity = await Task.Run(() => _projects.FirstOrDefault(p => p.Id == id));
        if (existingEntity == null) return null;
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        return existingEntity;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await Task.Run(()=>_projects.FirstOrDefault(p => p.Id == id));
        if (entity == null) return false;
        _projects.Remove(entity);
        return true;
    }
}