using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using Toolbox.DataAccess.Exceptions;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Entities;

namespace Toolbox.DataAccess.Uow
{
    public abstract class UnitOfWorkBase<TContext> : IUnitOfWorkBase where TContext : DbContext
    {
        protected internal UnitOfWorkBase(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        protected TContext _context;
        protected readonly IServiceProvider _serviceProvider;

        public int SaveChanges()
        {
            CheckDisposed();
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            CheckDisposed();
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            CheckDisposed();
            var repositoryType = typeof(IRepository<TEntity>);
            var repository = (IRepository<TEntity>)_serviceProvider.GetService(repositoryType);
			if ( repository == null )
			{
				throw new RepositoryNotFoundException(repositoryType.Name, String.Format("Repository {0} not found in the IOC container. Check if it is registered during startup.", repositoryType.Name));
			}

            ((IRepositoryInjection<TContext>)repository).SetContext(_context);
            return repository;
        }

        #region IDisposable Implementation

        protected bool _isDisposed;

        protected void CheckDisposed()
        {
            if ( _isDisposed ) throw new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if ( !_isDisposed )
            {
                if ( disposing )
                {
                    if ( _context != null )
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWorkBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
