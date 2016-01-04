using System;

namespace Toolbox.DataAccess.Options
{
    public abstract class ProviderDataAccessOptions
    {
        protected ProviderDataAccessOptions()
        { }

        public ConnectionString ConnectionString { get; protected set; }

        public DataAccessOptions DataAccessOptions { get; protected set; } = new DataAccessOptions();
    }
}
