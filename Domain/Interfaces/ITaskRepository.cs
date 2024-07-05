using Domain.Models;
using Domain.SearchParams;
using WebApplication1.Models;

namespace Domain.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskModel> GetByIdAsync(Guid id);
    Task<TaskModel> CreateAsync(TaskModel task);
    Task<TaskModel> UpdateAsync(Guid id, TaskModel task);
    Task DeleteAsync(Guid id);
}