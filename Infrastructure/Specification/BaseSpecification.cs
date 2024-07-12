using System.Linq.Expressions;
using Domain.Models;

namespace Infrastructure.Specification;

public abstract class BaseSpecification<TEntity>(Expression<Func<TEntity, bool>> criteria)
    where TEntity : BaseModel
{
    public Expression<Func<TEntity, bool>> Criteria { get; } = criteria;
    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
    
    protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}