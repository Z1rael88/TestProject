using Application.Dtos.TaskDtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskModel, TaskResponse>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
    }
}