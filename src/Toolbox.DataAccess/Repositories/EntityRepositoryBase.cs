using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toolbox.DataAccess.Entities;
using Toolbox.DataAccess.Exceptions;
using Toolbox.DataAccess.Query;

namespace Toolbox.DataAccess.Repositories
{
    public abstract class EntityRepositoryBase<TContext, TEntity> : RepositoryBase<TContext>, IRepository<TEntity> where TContext : DbContext where TEntity : EntityBase, new()
	{
		private readonly OrderBy<TEntity> DefaultOrderBy = new OrderBy<TEntity>(qry => qry.OrderBy(e => e.Id));

		protected EntityRepositoryBase(ILogger<DataAccess> logger, TContext context) : base(logger, context)
		{ }

		public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			var result = QueryDb(null, orderBy, includes);
			return result.ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			var result = QueryDb(null, orderBy, includes);
			return await result.ToListAsync();
		}

		public virtual IEnumerable<TEntity> GetPage(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			if ( orderBy == null ) orderBy = DefaultOrderBy.Expression;

			var result = QueryDb(null, orderBy, includes);
			return result.Skip(startRow).Take(pageLength).ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> GetPageAsync(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			if ( orderBy == null ) orderBy = DefaultOrderBy.Expression;

			var result = QueryDb(null, orderBy, includes);
			return await result.Skip(startRow).Take(pageLength).ToListAsync();
		}

		public virtual TEntity Get(int id, IncludeList<TEntity> includes = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (includes != null)
			{
				query = AddIncludes(query, includes);
			}

			return query.SingleOrDefault(x => x.Id == id);
		}

		public virtual Task<TEntity> GetAsync(int id, IncludeList<TEntity> includes = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (includes != null)
			{
				query = AddIncludes(query, includes);
			}

			return query.SingleOrDefaultAsync(x => x.Id == id);
		}

		public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			var result = QueryDb(filter, orderBy, includes);
			return result.ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			var result = QueryDb(filter, orderBy, includes);
			return await result.ToListAsync();
		}

		public virtual IEnumerable<TEntity> QueryPage(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			if ( orderBy == null ) orderBy = DefaultOrderBy.Expression;

			var result = QueryDb(filter, orderBy, includes);
			return result.Skip(startRow).Take(pageLength).ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> QueryPageAsync(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null)
		{
			if ( orderBy == null ) orderBy = DefaultOrderBy.Expression;

			var result = QueryDb(filter, orderBy, includes);
			return await result.Skip(startRow).Take(pageLength).ToListAsync();
		}

		public virtual void Add(TEntity entity)
		{
			if ( entity == null ) throw new InvalidOperationException("Unable to add a null entity to the repository.");
			Context.Set<TEntity>().Add(entity);
		}

		public virtual TEntity Update(TEntity entity)
		{
			var existing = Context.Set<TEntity>().Find(entity.Id);
			if ( existing == null ) throw new EntityNotFoundException(typeof(TEntity).Name, entity.Id);
			Context.Entry(existing).CurrentValues.SetValues(entity);
			return existing;
		}

		public virtual void Remove(TEntity entity)
		{
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
		}

		public virtual void Remove(int id)
		{
			var entity = new TEntity() { Id = id };
			this.Remove(entity);
		}

		public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.Count();
		}

		public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.CountAsync();
		}

		protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, IncludeList<TEntity> includes)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includes != null)
			{
				query = AddIncludes(query, includes);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			return query;
		}

		protected IQueryable<TEntity> AddIncludes(IQueryable<TEntity> query, IncludeList<TEntity> includes)
		{
			if (includes.Includes.Count() > 0)
			{
				foreach (var includeProperty in includes.Includes)
				{
					query = query.Include(includeProperty);
				}
			}
			return query;
		}

        public void SetUnchanged(TEntity entity)
        {
            base.Context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

    }
}