using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;
namespace WebApplication1.Repositories;

public class MockTaskRepository() : IMockTaskRepository
{
    private static List<TaskModel> _tasks =
    [
        new TaskModel { Id = Guid.NewGuid(), Title = "Task 1", Description = "Description 1", Status = Status.Started , ProjectId = Guid.Empty},
        new TaskModel { Id = Guid.NewGuid(), Title = "Task 2", Description = "Description 2", Status = Status.Completed ,ProjectId = Guid.Empty},
        new TaskModel { Id = Guid.NewGuid(), Title = "Task 3", Description = "Description 3", Status = Status.Started, ProjectId = Guid.Empty}
    ];

    public List<TaskModel> GetAll()
    {
        var allTasks= _tasks.ToList();
        return allTasks;
    }

    public TaskModel? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }
    public TaskModel Create(TaskModel entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _tasks.Add(entity);
        return entity;
    }

    public TaskModel? Update(Guid id, TaskModel entity)
    {
        var existingEntity = _tasks.FirstOrDefault(t => t.Id == id);

        if ( existingEntity == null || existingEntity.Id == Guid.Empty) return null;
        existingEntity.Title = entity.Title;
        existingEntity.Description = entity.Description;
        existingEntity.Status = entity.Status;
        existingEntity.ProjectId = entity.ProjectId;
        return existingEntity;
    }
    public bool Delete(Guid id)
    {
        var taskToDelete = _tasks.FirstOrDefault(p => p.Id == id);
        if (taskToDelete == null) return false;
        _tasks.Remove(taskToDelete);
        return true;
    }
}