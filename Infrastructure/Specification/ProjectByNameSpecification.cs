using Domain.Models;

namespace Infrastructure.Specification;

public class ProjectByNameSpecification : BaseSpecification<ProjectModel>
{
    public ProjectByNameSpecification(string? name)
    : base(project=>project.Name.Contains(name))
    {
        AddInclude(project=>project.Tasks);
    }
}