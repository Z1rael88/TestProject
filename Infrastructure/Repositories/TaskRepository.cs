using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaskRepository(IApplicationDbContext dbContext) : ITaskRepository
{
    public async Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams searchParams)
    {
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

            return allTasks;
        });
    }

    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        if (task == null)
        {
            throw new NotFoundException($"Task with {id} not found");
        }

        return task;
    }

    public async Task<TaskModel> CreateAsync(TaskModel task)
    {
        var newTask = dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
        return newTask.Entity;
    }

    public async Task<TaskModel> UpdateAsync(TaskModel task)
    {
        var updatedTask = dbContext.Tasks.Update(task);
        await dbContext.SaveChangesAsync();
        return updatedTask.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
    }


}