using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskRepository
{
    Task<ICollection<TaskModel>> GetAllAsync(SearchDto searchDto);//!!
    Task<TaskModel?> GetByIdAsync(Guid id);
    Task<TaskModel> CreateAsync(TaskModel taskEntity);
    Task<TaskModel> UpdateAsync(Guid id, TaskModel entity);
    Task<bool> DeleteAsync(Guid id);
}