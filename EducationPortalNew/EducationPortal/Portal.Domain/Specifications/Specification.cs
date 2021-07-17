using Portal.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Portal.Domain.Specifications
{
    public class Specification<TEntity>
        where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Expression { get; }

        public Func<TEntity, bool> Func => this.Expression.Compile();

        public Specification(Expression<Func<TEntity, bool>> expression)
        {
            this.Expression = expression;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return this.Func(entity);
        }
    }
}
