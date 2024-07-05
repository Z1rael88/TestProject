using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;

namespace Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private static ICollection<ProjectModel> _projects =
    [
        new ProjectModel
        {
            Id = Guid.NewGuid(),
            Name = "Project 1",
            Description = "Description 1",
            StartDate = DateOnly.MinValue,
            Tasks = []
        },
        new ProjectModel
        {
            Id = Guid.NewGuid(),
            Name = "Project 2",
            Description = "Description 2",
            StartDate = DateOnly.MinValue,
            Tasks = []
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

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        return (await Task.Run(() => _projects.FirstOrDefault(p => p.Id == id)))!;
    }

    public async Task<ProjectModel> CreateAsync(ProjectModel project)
    {
        if (project.Id == Guid.Empty)
        {
            project.Id = Guid.NewGuid();
        }

        await Task.Run(() => _projects.Add(project));
        return project;
    }

    public async Task<ProjectModel> UpdateAsync(Guid id, ProjectModel project)
    {
        var existingEntity = await GetByIdAsync(id);
        if (existingEntity == null) throw new NotFoundException("Project with that Id not found");
        return existingEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) throw new NotFoundException("Project with that Id not found");
        _projects.Remove(entity);
    }
}