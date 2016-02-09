using System.Data.Entity;
using Microsoft.Extensions.Logging;
using Toolbox.DataAccess.Entities;

namespace Toolbox.DataAccess.Repositories
{
    public class GenericEntityRepository<TEntity> : EntityRepositoryBase<DbContext, TEntity> where TEntity : EntityBase, new()
    {
		public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}