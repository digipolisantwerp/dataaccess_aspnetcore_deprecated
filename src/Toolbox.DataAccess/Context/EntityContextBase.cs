using System.Data.Entity;
using Microsoft.Extensions.OptionsModel;

namespace Toolbox.DataAccess.Context
{
    public class EntityContextBase : DbContext
    {
        public EntityContextBase(IOptions<DataAccessOptions> options) : base(options.Value.ConnectionString.ToString())
        {
            DataAccessOptions = options.Value;
            this.Configuration.LazyLoadingEnabled = DataAccessOptions.LazyLoadingEnabled;
        }

        internal DataAccessOptions DataAccessOptions { get; }
    }
}
