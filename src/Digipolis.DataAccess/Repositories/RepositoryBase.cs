using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Digipolis.DataAccess.Repositories
{
    public abstract class RepositoryBase<TContext> : IRepositoryInjection<TContext> where TContext : DbContext
    {
        protected RepositoryBase(ILogger<DataAccess> logger, TContext context)
        {
            this.Logger = logger;
            this.Context = context;
        }

        protected ILogger Logger { get; private set; }
        protected TContext Context { get; private set; }

        IRepositoryInjection<TContext> IRepositoryInjection<TContext>.SetContext(TContext context)
        {
            this.Context = context;
            return this;
        }
    }
}
