using System;
using Microsoft.Extensions.Logging;
using Toolbox.DataAccess.Context;

namespace Toolbox.DataAccess.Uow
{
    public class UowProvider : IUowProvider
    {

        public UowProvider()
        {

        }

        public UowProvider(ILogger<DataAccess> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        
        public IUnitOfWork CreateUnitOfWork(bool autoDetectChanges = true, bool enableLogging = false)
        {
            var _context = (EntityContextBase)_serviceProvider.GetService(typeof(EntityContextBase));
            _context.Configuration.AutoDetectChangesEnabled = autoDetectChanges;

            if (enableLogging)
            {
                _context.Database.Log = (msg) => _logger.LogVerbose(msg);
            }

            var uow = new UnitOfWork(_context, _serviceProvider);
            return uow;
        }
    }
}
