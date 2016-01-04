using System;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.Postgres.Options
{
    public class PostgresDataAccessOptions : ProviderDataAccessOptions
    {
        public void UsePostgres(ConnectionString connectionString, DataAccessOptions dataAccessOptions = null)
        {
            EntityContextOptionsBuilder.SetConnectionString(connectionString);

            var options = dataAccessOptions == null ? new DataAccessOptions() : dataAccessOptions;
            EntityContextOptionsBuilder.SetDataAccessOptions(options);
        }
    }
}
