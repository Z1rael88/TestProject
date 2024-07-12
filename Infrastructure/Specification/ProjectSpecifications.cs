using Domain.Models;

namespace Infrastructure.Specification;

public class ProjectSpecifications : BaseSpecification<ProjectModel>
{
    public ProjectSpecifications(string? name, string? description, DateOnly? startDate)
    {
        BuildCriteria(name, description, startDate);
    }

    private void BuildCriteria(string? name, string? description, DateOnly? startDate)
    {
        if (!string.IsNullOrEmpty(description))
        {
            AddCriteria(project => project.Description.Contains(description));
        }

        if (!string.IsNullOrEmpty(name))
        {
            AddCriteria(project => project.Name.Contains(name));
        }

        if (startDate.HasValue)
        {
            AddCriteria(project => project.StartDate >= startDate.Value);
        }
    }
}