using WebApplication1.Dtos.SearchParams;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskModel> GetByIdAsync(Guid id);
    Task<TaskModel> CreateAsync(TaskModel entity);
    Task<TaskModel> UpdateAsync(Guid id, TaskModel entity);
    Task DeleteAsync(Guid id);
}