using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class MockProjectRepository() : IMockProjectRepository
{
    private static List<ProjectModel> _projects =
    [
        new ProjectModel { Id = Guid.NewGuid(), Name = "Project 1", Description = "Description 1", StartDate = DateTime.Now, Tasks = [] },
        new ProjectModel { Id = Guid.NewGuid(), Name = "Project 2", Description = "Description 2", StartDate = DateTime.Now, Tasks = [] }
    ];

    public List<ProjectModel> GetAll()
    {
        var allProjects= _projects.ToList();
        return allProjects;
    }

    public ProjectModel? GetById(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        return project ?? null; ///not null, there is no project
    }

    public ProjectModel Create(ProjectModel entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _projects.Add(entity);
        return entity;
    }

    public ProjectModel? Update(Guid id, ProjectModel entity)
    {
        var existingEntity = _projects.FirstOrDefault(p => p.Id == id);
        if (existingEntity == null) return null;
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        return existingEntity;
    }

    public List<ProjectModel> Search(string searchTerm, string descriptionTerm)
    {
        var filteredProjects = _projects.ToList();
        
        if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrEmpty(descriptionTerm))
        {
           filteredProjects = filteredProjects
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (!string.IsNullOrEmpty(descriptionTerm))
        {
            filteredProjects = filteredProjects
                .Where(p => p.Name.Contains(descriptionTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (filteredProjects.Count == 0)
        {
            throw new NotFoundException("No projects found matching the search criteria.");
        }
        return filteredProjects;
    }

    public bool Delete(Guid id)
    {
        var entity = _projects.FirstOrDefault(p => p.Id == id);
        if (entity == null) return false;
        //entity.Tasks.Add();
        _projects.Remove(entity);
        return true;
    }
}