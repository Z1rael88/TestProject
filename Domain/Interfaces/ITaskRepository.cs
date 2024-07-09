using Domain.Models;
using Domain.SearchParams;

namespace Domain.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskModel> GetByIdAsync(Guid id);
    Task<TaskModel> CreateAsync(TaskModel task);
    Task<TaskModel> UpdateAsync(TaskModel task);
    Task DeleteAsync(Guid id);

}