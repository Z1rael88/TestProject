using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Specification;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQueryable,
        BaseSpecification<TEntity> specification) where TEntity : BaseModel
    {
        IQueryable<TEntity> queryable = inputQueryable;

        if (specification.Criteria is not null)
            queryable = queryable.Where(specification.Criteria);
        
        specification.Includes.Aggregate(queryable,
            (current, includeExpression) => current.Include(includeExpression));
        return queryable;
    }
}