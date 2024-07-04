using WebApplication1.Dto;
using WebApplication1.Dtos.TaskDtos;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskService
{
    public Task<ICollection<TaskResponse>> GetAllAsync();
    public Task<TaskResponse> GetByIdAsync(Guid id);
    public Task<TaskResponse> CreateAsync(TaskRequest taskRequest,Guid projectId);
    public Task<bool> DeleteAsync(Guid id);
    public Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest);
}