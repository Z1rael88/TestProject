using Application.Dtos.TaskDtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskModel, TaskResponse>();
        CreateMap<UpdateTaskRequest, TaskModel>();
        CreateMap<CreateTaskRequest, TaskModel>();
    }
}