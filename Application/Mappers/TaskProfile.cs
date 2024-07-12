using Application.Dtos.TaskDtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskModel, TaskResponse>();
        CreateMap<BaseTaskRequest, TaskModel>();
        CreateMap<CreateTaskRequest, TaskModel>();
    }
}