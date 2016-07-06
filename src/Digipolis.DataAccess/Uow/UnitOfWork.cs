using System;
using Digipolis.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Digipolis.DataAccess.Uow
{
    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}
