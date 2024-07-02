using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class ProjectMappers
{
    public static ProjectDto ProjectToDto(this Project project)
    {
        return new ProjectDto
        {
            Name = project.Name,
            Description = project.Description
        };
    }
}