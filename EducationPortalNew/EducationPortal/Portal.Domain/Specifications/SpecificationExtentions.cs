using Portal.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Portal.Domain.Specifications
{
    public static class SpecificationExtentions
    {
        public static Specification<TEntity> Or<TEntity>(this Specification<TEntity> left, Specification<TEntity> right)
            where TEntity : class
        {
            var leftExpr = left.Expression;
            var rightExpr = right.Expression;
            var leftParam = leftExpr.Parameters[0];
            var rightParam = rightExpr.Parameters[0];

            return new Specification<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.OrElse(
                        leftExpr.Body,
                        new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
                    leftParam));
        }

        public static Specification<TEntity> And<TEntity>(this Specification<TEntity> left, Specification<TEntity> right)
            where TEntity : class
        {
            var leftExpr = left.Expression;
            var rightExpr = right.Expression;
            var leftParam = leftExpr.Parameters[0];
            var rightParam = rightExpr.Parameters[0];

            return new Specification<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.AndAlso(
                        leftExpr.Body,
                        new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
                    leftParam));
        }

        public static Specification<TEntity> Not<TEntity>(this Specification<TEntity> specification)
            where TEntity : class
        {
            return new Specification<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Not(specification.Expression.Body),
                    specification.Expression.Parameters));
        }
    }
}
