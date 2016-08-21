using System;
using System.Linq.Expressions;
using Digipolis.DataAccess.Entities;

namespace Digipolis.DataAccess.Query
{
	public class Filter<TEntity>
	{
		public Filter(Expression<Func<TEntity, bool>> expression)
		{
			Expression = expression;
		}

		public Expression<Func<TEntity, bool>> Expression { get; private set; }

		public void AddExpression(Expression<Func<TEntity, bool>> newExpression)
		{
            if ( newExpression == null ) throw new ArgumentNullException(nameof(newExpression), $"{nameof(newExpression)} is null.");

			if ( Expression == null ) Expression = newExpression;

			var parameter = System.Linq.Expressions.Expression.Parameter(typeof(TEntity));

			var leftVisitor = new ReplaceExpressionVisitor(newExpression.Parameters[0], parameter);
			var left = leftVisitor.Visit(newExpression.Body);

			var rightVisitor = new ReplaceExpressionVisitor(Expression.Parameters[0], parameter);
			var right = rightVisitor.Visit(Expression.Body);

			Expression = System.Linq.Expressions.Expression.Lambda<Func<TEntity, bool>>(System.Linq.Expressions.Expression.AndAlso(left, right), parameter);
		}
	}
}