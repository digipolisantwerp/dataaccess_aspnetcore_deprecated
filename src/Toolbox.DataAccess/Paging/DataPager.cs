using System;
using System.Threading.Tasks;
using Toolbox.DataAccess.Entities;
using Toolbox.DataAccess.Query;
using Toolbox.DataAccess.Repositories;

namespace Toolbox.DataAccess.Paging
{
    public class DataPager<TEntity> : IDataPager<TEntity> where TEntity : EntityBase
    {
        public DataPager(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        private readonly IUowProvider _uowProvider;

        public DataPage<TEntity> Get(int pageNumber, int pageLength, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null)
        {
            using ( var uow = _uowProvider.CreateUnitOfWork(false) )
            {
                var repository = uow.GetRepository<TEntity>();

                var startRow = ( pageNumber - 1 ) * pageLength;
                var page = new DataPage<TEntity>()
                {
                    Data = repository.GetPage(startRow, pageLength, includes: includes, orderBy: orderby?.Expression),
                    TotalCount = repository.Count()
                };

                return page;
            }
        }

        public async Task<DataPage<TEntity>> GetAsync(int pageNumber, int pageLength, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null)
        {
            using ( var uow = _uowProvider.CreateUnitOfWork(false) )
            {
                var repository = uow.GetRepository<TEntity>();

                var startRow = ( pageNumber - 1 ) * pageLength;
                var page = new DataPage<TEntity>()
                {
                    Data = await repository.GetPageAsync(startRow, pageLength, includes: includes, orderBy: orderby?.Expression),
                    TotalCount = await repository.CountAsync()
                };

                return page;
            }
        }

        public DataPage<TEntity> Query(int pageNumber, int pageLength, Filter<TEntity> filter, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null)
        {
            using ( var uow = _uowProvider.CreateUnitOfWork(false) )
            {
                var repository = uow.GetRepository<TEntity>();

                var startRow = ( pageNumber - 1 ) * pageLength;
                var page = new DataPage<TEntity>()
                {
                    Data = repository.QueryPage(startRow, pageLength, filter.Expression, includes: includes, orderBy: orderby?.Expression),
                    TotalCount = repository.Count(filter.Expression)
                };

                return page;
            }
        }

        public async Task<DataPage<TEntity>> QueryAsync(int pageNumber, int pageLength, Filter<TEntity> filter, OrderBy<TEntity> orderby = null, IncludeList<TEntity> includes = null)
        {
            using ( var uow = _uowProvider.CreateUnitOfWork(false) )
            {
                var repository = uow.GetRepository<TEntity>();

                var startRow = ( pageNumber - 1 ) * pageLength;
                var page = new DataPage<TEntity>()
                {
                    Data = await repository.QueryPageAsync(startRow, pageLength, filter.Expression, includes: includes, orderBy: orderby?.Expression),
                    TotalCount = await repository.CountAsync(filter.Expression)
                };

                return page;
            }
        }
    }
}
