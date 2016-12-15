using Microsoft.EntityFrameworkCore;

namespace Digipolis.DataAccess.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}