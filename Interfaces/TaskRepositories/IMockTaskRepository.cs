using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.TaskRepositories;

public interface IMockTaskRepository
{
    List<TaskModel> GetAll();//!!
    TaskModel? GetById(Guid id);
    TaskModel Create(TaskModel taskEntity);
    TaskModel? Update(Guid id, TaskModel entity);
    bool Delete(Guid id);
}