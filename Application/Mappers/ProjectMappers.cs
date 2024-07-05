using Application.Dtos.ProjectDtos;
using Domain.Models;

namespace Application.Mappers;

public static class ProjectMappers
{
    public static ProjectResponse ToResponse(this ProjectModel project)
    {
        return new ProjectResponse()
        {
            Id = project.Id,
            Description = project.Description,
            StartDate = project.StartDate,
            Name = project.Name,
            Tasks = project.Tasks.Select(p => p.ToResponse())
        };
    }
}