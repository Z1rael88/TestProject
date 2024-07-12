using Domain.Models;

namespace Infrastructure.Specification;

internal class ProjectsByIdWithTasksSpecification : BaseSpecification<ProjectModel>
{
    public ProjectsByIdWithTasksSpecification(Guid projectId)
    : base(project=>project.Id == projectId)
    {
        AddInclude(project=>project.Tasks);
    }
}