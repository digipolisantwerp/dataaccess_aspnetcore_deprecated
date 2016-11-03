using System;
using System.Threading;
using System.Threading.Tasks;
using Digipolis.DataAccess.Entities;
using Digipolis.DataAccess.Repositories;

namespace Digipolis.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();
        TRepository GetCustomRepository<TRepository>();
    }
}
