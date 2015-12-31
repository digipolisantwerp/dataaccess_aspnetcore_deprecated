using System;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.Postgres.Options
{
    public class PostgresDataAccessOptions : ProviderDataAccessOptions
    {
        public ConnectionString ConnectionString { get; private set; }

        public void UsePostgres(ConnectionString connectionString)
        {
            if ( connectionString == null ) throw new ArgumentNullException(nameof(connectionString), $"{nameof(connectionString)} cannot be null.");
            ConnectionString = connectionString;
        }
    }
}
