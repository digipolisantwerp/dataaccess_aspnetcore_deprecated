using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Digipolis.DataAccess.Entities;
using Digipolis.DataAccess.Query;

namespace Digipolis.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
	{
		IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);
		Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);

        IEnumerable<TEntity> GetPage(int startRij, int aantal, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);
		Task<IEnumerable<TEntity>> GetPageAsync(int startRij, int aantal, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);

		TEntity Get(int id, IncludeList<TEntity> includes = null);
		Task<TEntity> GetAsync(int id, IncludeList<TEntity> includes = null);

		IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);
		Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);

		IEnumerable<TEntity> QueryPage(int startRij, int aantal, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);
		Task<IEnumerable<TEntity>> QueryPageAsync(int startRij, int aantal, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);

        void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);
        Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IncludeList<TEntity> includes = null);

        void Add(TEntity entity);

		TEntity Update(TEntity entity);

		void Remove(TEntity entity);
		void Remove(int id);

		int Count(Expression<Func<TEntity, bool>> filter = null);
		Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        void SetUnchanged(TEntity entitieit);
    }
}
