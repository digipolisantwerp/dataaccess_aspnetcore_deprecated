using System;
using System.Threading.Tasks;
using Digipolis.DataAccess.Entities;
using Digipolis.DataAccess.Query;

namespace Digipolis.DataAccess.Paging
{
    public interface IDataPager<TEntity> where TEntity : EntityBase
    {
        DataPage<TEntity> Get(int pageNumber, int pageLength, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null);
        DataPage<TEntity> Query(int pageNumber, int pageLength, Filter<TEntity> filter, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null);

        Task<DataPage<TEntity>> GetAsync(int pageNumber, int pageLength, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null);
        Task<DataPage<TEntity>> QueryAsync(int pageNumber, int pageLength, Filter<TEntity> filter, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null);
    }
}
