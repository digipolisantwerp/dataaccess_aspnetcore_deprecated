using System;
using Microsoft.Extensions.Logging;
using Digipolis.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Digipolis.DataAccess.Uow
{
    public class UowProvider : IUowProvider
    {
        public UowProvider()
        { }

        public UowProvider(ILogger<DataAccess> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        
        public IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false)
        {
            var _context = (DbContext)_serviceProvider.GetService(typeof(IEntityContext));

            if ( !trackChanges )
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // -- Not (yet) implemented in EF Core -- //
            //if (enableLogging)
            //{
            //    _context.Database.Log = (msg) => _logger.LogVerbose(msg);
            //}

            var uow = new UnitOfWork(_context, _serviceProvider);
            return uow;
        }
    }
}
