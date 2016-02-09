using System;
using System.Threading;
using System.Threading.Tasks;
using Toolbox.DataAccess.Entities;
using Toolbox.DataAccess.Repositories;

namespace Toolbox.DataAccess.Uow
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;
    }
}
