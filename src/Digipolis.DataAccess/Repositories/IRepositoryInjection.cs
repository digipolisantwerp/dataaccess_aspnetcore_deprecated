using Microsoft.EntityFrameworkCore;

namespace Digipolis.DataAccess.Repositories
{
    public interface IRepositoryInjection<TContext> where TContext : DbContext
    {
        IRepositoryInjection<TContext> SetContext(TContext context);
    }
}