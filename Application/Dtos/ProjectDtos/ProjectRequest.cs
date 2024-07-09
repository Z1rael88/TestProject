namespace Application.Dtos.ProjectDtos;

public class ProjectRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly StartDate { get; set; }
}