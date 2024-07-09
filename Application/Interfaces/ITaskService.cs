using Application.Dtos.TaskDtos;
using Domain.SearchParams;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskResponse> GetByIdAsync(Guid id);
    Task<TaskResponse> CreateAsync(CreateTaskRequest createTaskRequest);
    Task DeleteAsync(Guid id);
    Task<TaskResponse> UpdateAsync(Guid id, UpdateTaskRequest updateTaskRequest);
}