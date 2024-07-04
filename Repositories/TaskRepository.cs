using WebApplication1.Dtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Models;
namespace WebApplication1.Repositories;

public class TaskRepository(IProjectRepository projectRepository) : ITaskRepository
{
    private static List<TaskModel> _tasks =
    [
        new TaskModel { Id = Guid.NewGuid(), Title = "Task 1", Description = "Description 1", Status = Status.Started , ProjectId = Guid.Empty},
        new TaskModel { Id = Guid.NewGuid(), Title = "Task 2", Description = "Description 2", Status = Status.Completed ,ProjectId = Guid.Empty},
        new TaskModel { Id = Guid.NewGuid(), Title = "Task 3", Description = "Description 3", Status = Status.Started, ProjectId = Guid.Empty}
    ];

    public async Task<ICollection<TaskModel>> GetAllAsync(SearchDto searchDto)
    {
        return await Task.Run(() => _tasks);
    }

    public async Task<TaskModel?> GetByIdAsync(Guid id)
    {
        return await Task.Run(() => _tasks.FirstOrDefault(t => t.Id == id));
    }
    public async Task<TaskModel> CreateAsync(TaskModel entity)
    {
        var project = await projectRepository.GetByIdAsync(entity.ProjectId);
        if (project == null)
        {
            throw new Exception();
        }
        
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _tasks.Add(entity);
        project.Tasks.Add(entity);
        return entity;
    }

    public async Task<TaskModel> UpdateAsync(Guid id, TaskModel entity)
    {
        var existingEntity = await Task.Run(() => _tasks.FirstOrDefault(t => t.Id == id));
        if (existingEntity == null || existingEntity.Id == Guid.Empty) throw new NotFoundException();
        existingEntity.Title = entity.Title;
        existingEntity.Description = entity.Description;
        existingEntity.Status = entity.Status;
        existingEntity.ProjectId= entity.ProjectId;
        return existingEntity;
    }
    public async Task DeleteAsync(Guid id)
    {
        var taskToDelete = await Task.Run(()=>_tasks.FirstOrDefault(p => p.Id == id));
        _tasks.Remove(taskToDelete);
    }
}