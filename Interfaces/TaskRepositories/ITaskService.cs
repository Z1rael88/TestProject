using WebApplication1.Dtos.SearchParams;
using WebApplication1.Dtos.TaskDtos;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams);
    Task<TaskResponse> GetByIdAsync(Guid id);
    Task<TaskResponse> CreateAsync(TaskRequest taskRequest, Guid projectId);
    Task DeleteAsync(Guid id);
    Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest);
}