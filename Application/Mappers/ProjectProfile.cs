using Application.Dtos.ProjectDtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectModel, ProjectResponse>();
        CreateMap<ProjectRequest, ProjectModel>();
    }
}