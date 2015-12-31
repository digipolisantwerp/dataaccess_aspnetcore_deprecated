using System;

namespace Toolbox.DataAccess.Options
{
    public abstract class ProviderDataAccessOptions
    {
        public DataAccessOptions DataAccessOptions { get; set; } = new DataAccessOptions();
    }
}
