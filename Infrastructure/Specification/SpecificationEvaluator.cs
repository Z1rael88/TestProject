using System.Linq;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQueryable,
            BaseSpecification<TEntity> specification) where TEntity : BaseModel
        {
            var queryable = specification.Criteria.Aggregate(inputQueryable, (current, criteria) => current.Where(criteria));

            queryable = specification.Includes
                .Aggregate(queryable,
                    (current, includeExpression) => current.Include(includeExpression));

            return queryable;
        }
    }
}