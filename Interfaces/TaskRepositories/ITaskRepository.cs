using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskRepository
{
    Task<ICollection<TaskModel>> GetAllAsync();//!!
    Task<TaskModel?> GetByIdAsync(Guid id);
    Task<TaskModel> CreateAsync(TaskModel taskEntity);
    Task<TaskModel> UpdateAsync(Guid id, TaskModel entity);
    Task<bool> DeleteAsync(Guid id);
}