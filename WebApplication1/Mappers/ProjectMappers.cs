using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class ProjectMappers
{
    public static ProjectResponse ProjectToResponse(this ProjectModel project)
    {
        return new ProjectResponse()
        {
            Id = project.Id,
            Description = project.Description,
            StartDate = project.StartDate,
            Name = project.Name,
            Tasks = project.Tasks.Select(p => p.TaskToResponse()).ToList()
        };
    }

    public static ProjectModel ProjectRequestToTaskModel(this ProjectRequest request)
    {
        return new ProjectModel
        {
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
        };
    }
}