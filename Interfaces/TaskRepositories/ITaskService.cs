using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.TaskDtos;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface ITaskService
{
    public List<TaskResponse?> GetAll(SearchDto searchDto);
    public TaskResponse? GetById(Guid id);
    public TaskResponse? Create(TaskRequest taskRequest,Guid projectId);
    public bool Delete(Guid id);
    public TaskResponse? Update(Guid id, TaskRequest taskRequest);
}