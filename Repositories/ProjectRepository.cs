using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ProjectRepository() : IProjectRepository
{
    private static IList<ProjectModel> _projects =
    [
        new ProjectModel { Id = Guid.NewGuid(), Name = "Project 1", Description = "Description 1", StartDate = DateTime.Now, Tasks = new List<TaskModel>() },
        new ProjectModel { Id = Guid.NewGuid(), Name = "Project 2", Description = "Description 2", StartDate = DateTime.Now, Tasks = new List<TaskModel>() }
    ];

    public async Task<ICollection<ProjectModel>> GetAllAsync()
    {
        return await Task.Run(() => _projects.AsReadOnly());
    }

    public async Task<ProjectModel?> GetByIdAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
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
        if (existingEntity == null) throw new ArgumentException();
        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        return existingEntity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await Task.Run(() => _projects.FirstOrDefault(p => p.Id == id));
        if (entity == null) return false;
        _projects.Remove(entity);
        return true;
    }
}