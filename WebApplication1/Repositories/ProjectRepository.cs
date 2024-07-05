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
            Id = Guid.NewGuid(), Name = "Project 1", Description = "Description 1", StartDate = DateOnly.MinValue, Tasks = []
        },
        new ProjectModel
        {
            Id = Guid.NewGuid(), Name = "Project 2", Description = "Description 2", StartDate = DateOnly.MinValue, Tasks = []
        }
    ];

    public async Task<IEnumerable<ProjectModel>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        return await Task.Run(() =>
        {
            var allProjects = _projects.AsQueryable();
            if (!string.IsNullOrEmpty(projectSearchParams.Name))
            {
                allProjects = allProjects
                    .Where(p => p.Name.Contains(projectSearchParams.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(projectSearchParams.Description))
            {
                allProjects = allProjects
                    .Where(p => p.Description.Contains(projectSearchParams.Description,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (projectSearchParams.StartDate.HasValue)
            {
                allProjects = allProjects
                    .Where(p => p.StartDate == projectSearchParams.StartDate.Value);
            }
            if (projectSearchParams.StartDateFrom.HasValue)
            {
                allProjects = allProjects
                    .Where(p => p.StartDate >= projectSearchParams.StartDateFrom.Value);
            }

            if (projectSearchParams.StartDateTo.HasValue)
            {
                allProjects = allProjects
                    .Where(p => p.StartDate <= projectSearchParams.StartDateTo.Value);
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
        return existingEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.Run(() =>
        {
            var entity =  _projects.FirstOrDefault(p => p.Id == id);
            if (entity == null) throw new NotFoundException();
            _projects.Remove(entity);
        });
    }
}