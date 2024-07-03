using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class ProjectMappers
{
    public static ProjectResponse ProjectToResponse(this ProjectModel projectModel)
    {
        return new ProjectResponse
        {
            Id = projectModel.Id,
            Name = projectModel.Name,
            Description = projectModel.Description,
            StartDate = projectModel.StartDate,
            Tasks = projectModel.Tasks.Select(t=>t.TaskToResponse())
        };
    }
    public static ProjectRequest ProjectToRequest(this ProjectModel projectModel)
    {
        return new ProjectRequest
        {
            Name = projectModel.Name,
            Description = projectModel.Description,
            StartDate = projectModel.StartDate
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