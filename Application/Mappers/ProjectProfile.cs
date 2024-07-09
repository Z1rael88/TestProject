using Application.Dtos.ProjectDtos;
using Application.Dtos.TaskDtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectModel, ProjectResponse>()
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks.Select(task => new TaskResponse
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                ProjectId = task.ProjectId
            })));
    }
}