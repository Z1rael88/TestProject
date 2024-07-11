using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;

namespace Infrastructure.Repositories;

public class TaskRepository(IApplicationDbContext dbContext, ILogger<TaskRepository> logger) : ITaskRepository
{
    public async Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams searchParams)
    {
        logger.LogInformation("Started retrieving tasks from database");
        return await Task.Run(() =>
        {
            var allTasks = dbContext.Tasks.AsQueryable();
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

            logger.LogInformation("Successfully retrieved tasks from database");
            return allTasks.AsNoTracking();
        });
    }

    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Started retrieving task from database");
        var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        if (task == null)
        {
            throw new NotFoundException($"Task with {id} not found");
        }

        logger.LogInformation("Successfully retrieved task from database");
        return task;
    }

    public async Task<TaskModel> CreateAsync(TaskModel task)
    {
        logger.LogInformation("Started creating task from database");
        var newTask = dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Successfully created task from database");
        return newTask.Entity;
    }

    public async Task<TaskModel> UpdateAsync(TaskModel task)
    {
        logger.LogInformation("Started updating task from database");
        var updatedTask = dbContext.Tasks.Update(task);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Successfully updated task from database");
        return updatedTask.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation("Started deleting task from database");
        var task = await GetByIdAsync(id);
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Successfully deleted task from database");
    }
}