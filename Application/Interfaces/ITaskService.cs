using Application.Dtos.TaskDtos;
using Domain.SearchParams;

namespace WebApplication1.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskResponse> GetByIdAsync(Guid id);
    Task<TaskResponse> CreateAsync(TaskRequest taskRequest, Guid projectId);
    Task DeleteAsync(Guid id);
    Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest);
}