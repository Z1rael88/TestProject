using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.TaskDtos;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskService
{
     Task<ICollection<TaskResponse?>> GetAllAsync(SearchDto searchDto);
     Task<TaskResponse?> GetByIdAsync(Guid id);
     Task<TaskResponse> CreateAsync(TaskRequest taskRequest,Guid projectId);
     Task<bool> DeleteAsync(Guid id);
     Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest);
}