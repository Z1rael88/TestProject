using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Models;

namespace Infrastructure.Specification
{
    public abstract class BaseSpecification<TEntity> where TEntity : BaseModel
    {
        protected List<Expression<Func<TEntity, bool>>> _criteria = new List<Expression<Func<TEntity, bool>>>();
        protected List<Expression<Func<TEntity, object>>> _includes = new List<Expression<Func<TEntity, object>>>();

        public IReadOnlyList<Expression<Func<TEntity, bool>>> Criteria => _criteria;
        public IReadOnlyList<Expression<Func<TEntity, object>>> Includes => _includes;
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            _includes.Add(includeExpression);
        }

        protected void AddCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            _criteria.Add(criteria);
        }
    }
}