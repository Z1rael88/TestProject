using Application.Dtos.TaskDtos;
using Domain.SearchParams;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskResponse> GetByIdAsync(Guid id);
    Task<TaskResponse> CreateAsync(CreateTaskRequest taskRequest);
    Task DeleteAsync(Guid id);
    Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest);
}