using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;

namespace Infrastructure.Repositories;

public class TaskRepository(IProjectRepository projectRepository) : ITaskRepository
{
    private static ICollection<TaskModel> _tasks =
    [
        new TaskModel
        {
            Id = Guid.NewGuid(),
            Name = "Task 1",
            Description = "Description 1",
            Status = Status.Started,
            ProjectId = Guid.Empty
        },
        new TaskModel
        {
            Id = Guid.NewGuid(),
            Name = "Task 2",
            Description = "Description 2",
            Status = Status.Completed,
            ProjectId = Guid.Empty
        },
        new TaskModel
        {
            Id = Guid.NewGuid(),
            Name = "Task 3",
            Description = "Description 3",
            Status = Status.Started,
            ProjectId = Guid.Empty
        }
    ];

    public async Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams searchParams)
    {
        return await Task.Run(() =>
        {
            var allTasks = _tasks.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.Name))
            {
                allTasks = allTasks
                    .Where(p => p.Name.Contains(searchParams.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchParams.Description))
            {
                allTasks = allTasks
                    .Where(
                        p => p.Description.Contains(searchParams.Description, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.Status != null)
            {
                allTasks = allTasks
                    .Where(p => p.Status == searchParams.Status);
            }

            if (searchParams.ProjectId != null)
            {
                allTasks = allTasks
                    .Where(p => p.ProjectId == searchParams.ProjectId);
            }

            return allTasks;
        });
    }

    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        return await Task.Run(() =>
        {
            var task = _tasks.FirstOrDefault(p => p.Id == id);
            if (task == null)
            {
                throw new NotFoundException($"Task with {id} not found");
            }

            return task;
        });
    }

    public async Task<TaskModel> CreateAsync(TaskModel task)
    {
        var project = await projectRepository.GetByIdAsync(task.ProjectId);
        if (task.Id == Guid.Empty)
        {
            task.Id = Guid.NewGuid();
        }

        await Task.Run(() =>
        {
            _tasks.Add(task);
            project.Tasks.Add(task);
        });
        return task;
    }

    public async Task<TaskModel> UpdateAsync(TaskModel task)
    {
        var existingTask = await GetByIdAsync(task.Id);
        return existingTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        var project = await projectRepository.GetByIdAsync(task.ProjectId);
        project.Tasks.Remove(task);
        _tasks.Remove(task);
    }
}