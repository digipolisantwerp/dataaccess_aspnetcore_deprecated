using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Digipolis.DataAccess.Entities;

namespace Digipolis.DataAccess.Query
{
    public class IncludeList<TEntity> where TEntity : EntityBase
    {
        public IncludeList()
        {
            _includes = new List<Expression<Func<TEntity, object>>>();
        }

		public IncludeList(params Expression<Func<TEntity, object>>[] includes)
		{
			_includes = new List<Expression<Func<TEntity, object>>>(includes);
		}

        private readonly List<Expression<Func<TEntity, object>>> _includes;

        public IEnumerable<Expression<Func<TEntity, object>>> Includes
        {
            get { return _includes.AsEnumerable(); }
        }

        public IncludeList<TEntity> Add(Expression<Func<TEntity, object>> include)
        {
            if ( include == null ) return this;
            _includes.Add(include);
            return this;
        }
    }
}