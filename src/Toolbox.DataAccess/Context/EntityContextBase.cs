using System.Data.Entity;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.Entiteiten
{
    public class EntityContextBase : DbContext
    {
        private readonly DataAccessOptions _dataAccessOptions;

        public EntityContextBase(DataAccessOptions dataAccessOptions) : base(dataAccessOptions.ConnectionString.ToString())
        {
            _dataAccessOptions = dataAccessOptions;
            this.Configuration.LazyLoadingEnabled = false;
        }

        internal DataAccessOptions DataAccessOptions
        {
            get
            {
                return _dataAccessOptions;
            }
        }
    }
}
