using System.Linq.Expressions;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Specification
{
    public abstract class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
        where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get; } = criteria;
        public List<Expression<Func<T, object>>> Includes { get; } = [];

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}