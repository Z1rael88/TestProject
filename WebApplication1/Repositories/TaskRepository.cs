using WebApplication1.Dtos.SearchParams;
using WebApplication1.Enums;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class TaskRepository(IProjectRepository projectRepository) : ITaskRepository
{
    private static ICollection<TaskModel> _tasks =
    [
        new TaskModel
        {
            Id = Guid.NewGuid(), Name = "Task 1", Description = "Description 1", Status = Status.Started,
            ProjectId = Guid.Empty
        },
        new TaskModel
        {
            Id = Guid.NewGuid(), Name = "Task 2", Description = "Description 2", Status = Status.Completed,
            ProjectId = Guid.Empty
        },
        new TaskModel
        {
            Id = Guid.NewGuid(), Name = "Task 3", Description = "Description 3", Status = Status.Started,
            ProjectId = Guid.Empty
        }
    ];

    public async Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams searchParams)
    {
        return await Task.Run(() =>
        {
            var allTasks = _tasks.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.NameTerm))
            {
                allTasks = allTasks
                    .Where(p => p.Name.Contains(searchParams.NameTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchParams.DescriptionTerm))
            {
                allTasks = allTasks
                    .Where(
                        p => p.Description.Contains(searchParams.DescriptionTerm, StringComparison.OrdinalIgnoreCase));
            }

            return allTasks;
        });
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
            throw new NotFoundException();
        }

        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        await Task.Run(() =>
        {
            _tasks.Add(entity);
            project.Tasks.Add(entity);
        });
        return entity;
    }

    public async Task<TaskModel> UpdateAsync(Guid id, TaskModel entity)
    {
        var existingEntity = await Task.Run(() => _tasks.FirstOrDefault(t => t.Id == id));
        if (existingEntity == null || existingEntity.Id == Guid.Empty) throw new NotFoundException();

        return existingEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
         await Task.Run(async() =>
        {
            var entity =  _tasks.FirstOrDefault(p => p.Id == id);
            if (entity == null) throw new NotFoundException();
            var project = await projectRepository.GetByIdAsync(entity.ProjectId);
            if (project == null) throw new NotFoundException();
             project.Tasks.Remove(entity);
            _tasks.Remove(entity);
        });
    }
}