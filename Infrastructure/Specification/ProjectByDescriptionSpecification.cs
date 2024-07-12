using System.Linq.Expressions;
using Domain.Models;

namespace Infrastructure.Specification;

public class ProjectByDescriptionSpecification : BaseSpecification<ProjectModel>
{
    public ProjectByDescriptionSpecification(string? description)
        : base(  project=>project.Description.Contains(description))
    {
        AddInclude(project => project.Tasks);
    }

}