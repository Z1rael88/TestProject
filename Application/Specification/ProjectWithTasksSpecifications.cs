using System.Linq.Expressions;
using Domain.Extensions;
using Domain.Models;
using Domain.SearchParams;
using Microsoft.IdentityModel.Tokens;

namespace Application.Specification;

public class ProjectWithTasksSpecifications : BaseSpecification<ProjectModel>
{
    public ProjectWithTasksSpecifications(ProjectSearchParams projectSearchParams)
        : base(BuildCriteria(projectSearchParams))
    {
        AddInclude(project => project.Tasks);
    }

    private static Expression<Func<ProjectModel, bool>> BuildCriteria(ProjectSearchParams projectSearchParams)
    {
        Expression<Func<ProjectModel, bool>> criteria = x => true;

        if (!projectSearchParams.Name.IsNullOrEmpty())
        {
            criteria = criteria.AndAlso(x => x.Name.Contains(projectSearchParams.Name));
        }

        if (!projectSearchParams.Description.IsNullOrEmpty())
        {
            criteria = criteria.AndAlso(x => x.Description.Contains(projectSearchParams.Description));
        }

        if (projectSearchParams.StartDate.HasValue)
        {
            criteria = criteria.AndAlso(x => x.StartDate == projectSearchParams.StartDate);
        }

        return criteria;
    }
}