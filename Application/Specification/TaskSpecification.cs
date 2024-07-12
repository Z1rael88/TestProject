using System.Linq.Expressions;
using Domain.Extensions;
using Domain.Models;
using Domain.SearchParams;
using Microsoft.IdentityModel.Tokens;

namespace Application.Specification
{
    public class TaskSpecification : BaseSpecification<TaskModel>
    {
        public TaskSpecification(TaskSearchParams taskSearchParams)
            : base(BuildCriteria(taskSearchParams))
        {
        }

        private static Expression<Func<TaskModel, bool>> BuildCriteria(TaskSearchParams taskSearchParams)
        {
            Expression<Func<TaskModel, bool>> criteria = x => true;

            if (!taskSearchParams.Name.IsNullOrEmpty())
            {
                criteria = criteria.AndAlso(x => x.Name.Contains(taskSearchParams.Name));
            }

            if (!taskSearchParams.Description.IsNullOrEmpty())
            {
                criteria = criteria.AndAlso(x => x.Description.Contains(taskSearchParams.Description));
            }

            if (taskSearchParams.Status.HasValue)
            {
                var statusValue = taskSearchParams.Status.Value; // Get the enum value
                criteria = criteria.AndAlso(x => x.Status == statusValue);
            }

            return criteria;
        }
    }
}