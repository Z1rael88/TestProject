using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository(IApplicationDbContext dbContext) : IProjectRepository
{
    public async Task<IEnumerable<ProjectModel>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
            var allProjects = dbContext.Projects.AsQueryable();
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

            allProjects = allProjects.Include(p => p.Tasks);
            return await allProjects.ToListAsync();
    }

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project == null)
        {
            throw new NotFoundException($"Project with {id} not found");
        }

        return project;
    }

    public async Task<ProjectModel> CreateAsync(ProjectModel project)
    {
        if (project.Id == Guid.Empty)
        {
            project.Id = Guid.NewGuid();
        }

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<ProjectModel> UpdateAsync(ProjectModel project)
    {
        var existingEntity = await GetByIdAsync(project.Id);
        await dbContext.SaveChangesAsync();
        return existingEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await GetByIdAsync(id);
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
    }
}