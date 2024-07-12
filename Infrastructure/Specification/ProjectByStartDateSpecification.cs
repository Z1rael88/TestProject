using Domain.Models;

namespace Infrastructure.Specification;

public class ProjectByStartDateSpecification : BaseSpecification<ProjectModel>
{
    public ProjectByStartDateSpecification(DateOnly? startDate)
    : base(project=>project.StartDate == startDate.Value)
    {
        AddInclude(project=>project.Tasks);
    }
}