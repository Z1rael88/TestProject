using WebApplication1.Dtos.SearchParams;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ProjectRepository : IProjectRepository
{
    private static ICollection<ProjectModel> _projects =
    [
        new ProjectModel
        {
            Id = Guid.NewGuid(), Name = "Project 1", Description = "Description 1", StartDate = DateTime.Now, Tasks = []
        },
        new ProjectModel
        {
            Id = Guid.NewGuid(), Name = "Project 2", Description = "Description 2", StartDate = DateTime.Now, Tasks = []
        }
    ];

    public async Task<IEnumerable<ProjectModel>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        return await Task.Run(() =>
        {
            var allProjects = _projects.AsQueryable();
            if (!string.IsNullOrEmpty(projectSearchParams.NameTerm))
            {
                allProjects = allProjects
                    .Where(p => p.Name.Contains(projectSearchParams.NameTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(projectSearchParams.DescriptionTerm))
            {
                allProjects = allProjects
                    .Where(p => p.Description.Contains(projectSearchParams.DescriptionTerm,
                        StringComparison.OrdinalIgnoreCase));
            }

            return allProjects;
        });
    }

    public async Task<ProjectModel?> GetByIdAsync(Guid id)
    {
        return await Task.Run(() => _projects.FirstOrDefault(p => p.Id == id));
    }

    public async Task<ProjectModel> CreateAsync(ProjectModel entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        await Task.Run(() => _projects.Add(entity));
        return entity;
    }

    public async Task<ProjectModel> UpdateAsync(Guid id, ProjectModel entity)
    {
        var existingEntity = await Task.Run(() => _projects.FirstOrDefault(p => p.Id == id));
        if (existingEntity == null) throw new NotFoundException();
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        return existingEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await Task.Run(() => _projects.FirstOrDefault(p => p.Id == id));
        if (entity == null) throw new NotFoundException();
        _projects.Remove(entity);
    }
}