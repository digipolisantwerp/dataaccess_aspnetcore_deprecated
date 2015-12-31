using System.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.Entiteiten
{
    public class EntityContextBase : DbContext
    {
        private readonly DataAccessOptions _dataAccessOptions;

        public EntityContextBase(IOptions<EntityContextOptions> options) : base(options.Value.ConnectionString.ToString())
        {
            EntityContextOptions = options.Value;
            this.Configuration.LazyLoadingEnabled = EntityContextOptions.LazyLoadingEnabled;
        }

        internal EntityContextOptions EntityContextOptions { get; }
    }
}
