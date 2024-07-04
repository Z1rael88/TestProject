using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class ProjectMappers
{
    
    public static async  Task<ProjectResponse> ProjectToResponseAsync(this Task<ProjectModel> project)
    {
        ProjectModel projectModel = await project.ConfigureAwait(false);
        if (projectModel == null)
        {
            throw new ArgumentNullException(nameof(projectModel), "Task model cannot be null.");
        }

        return new ProjectResponse()
        {
            Id = projectModel.Id,
            Description = projectModel.Description,
            StartDate = projectModel.StartDate,
            Name = projectModel.Name,
            Tasks = projectModel.Tasks.Select(p=>p.TaskToResponse()).ToList()
        };
    }
    public static ProjectResponse ProjectToResponse(this ProjectModel project)
    {
        return new ProjectResponse()
        {
            Id = project.Id,
            Description = project.Description,
            StartDate = project.StartDate,
            Name = project.Name,
            Tasks = project.Tasks.Select(p=>p.TaskToResponse()).ToList()
        };
    }
    public static async Task<ProjectRequest> ProjectToRequest(this Task<ProjectModel> project)
    {
        var projectModel = await project;
        return new ProjectRequest
        {
            Name = projectModel.Name,
            Description = projectModel.Description,
            StartDate = projectModel.StartDate
        };
    }
    public static async  Task<ProjectModel> ProjectRequestToTaskModelAsync(this Task<ProjectRequest> request)
    {
        var projectModel = await request;
        return new ProjectModel
        {
            Name = projectModel.Name,
            Description = projectModel.Description,
            StartDate = projectModel.StartDate,
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