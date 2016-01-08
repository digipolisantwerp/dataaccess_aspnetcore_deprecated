using System.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.Context
{
    public class EntityContextBase : DbContext
    {
        public EntityContextBase(IOptions<EntityContextOptions> options) : base(options.Value.ConnectionString.ToString())
        {
            EntityContextOptions = options.Value;
            this.Configuration.LazyLoadingEnabled = EntityContextOptions.LazyLoadingEnabled;
        }

        protected EntityContextOptions EntityContextOptions { get; }
    }
}
