using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class ProjectRepository(IApplicationDbContext dbContext, ILogger<ProjectRepository> logger) : IProjectRepository
{
    public async Task<IEnumerable<ProjectModel>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        logger.LogInformation("Started retrieving projects from database");

        var allProjects = dbContext.Projects.AsQueryable();
        if (!string.IsNullOrEmpty(projectSearchParams.Name))
        {
            allProjects = allProjects
                .Where(p => p.Name.Contains(projectSearchParams.Name));
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
        logger.LogInformation("Successfully retrieved projects from database");
        return await allProjects.AsNoTracking().ToListAsync();
    }

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Started retrieving project from database");
        var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project == null)
        {
            throw new NotFoundException($"Project with {id} not found");
        }

        logger.LogInformation("Successfully retrieved project from database");
        return project;
    }

    public async Task<ProjectModel> CreateAsync(ProjectModel project)
    {
        logger.LogInformation("Started creating project from database");
        var newProject = dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Successfully created project from database");
        return newProject.Entity;
    }

    public async Task<ProjectModel> UpdateAsync(ProjectModel project)
    {
        logger.LogInformation("Started updating project from database");
        var updatedProject = dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Successfully updated project from database");
        return updatedProject.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation("Started deleting project from database");
        var project = await GetByIdAsync(id);
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Successfully deleted project from database");
    }

    public async Task<bool> IsProjectExistsAsync(Guid projectId)
    {
        logger.LogInformation($"Started checking if there is project with {projectId} in database");
        var response = await dbContext.Projects.AnyAsync(p => p.Id == projectId);
        logger.LogInformation($"Successfully found project with {projectId} in database");
        return response;
    }
}