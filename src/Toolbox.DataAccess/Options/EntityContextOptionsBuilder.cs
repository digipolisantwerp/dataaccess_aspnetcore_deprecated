using System;

namespace Toolbox.DataAccess.Options
{
    public class EntityContextOptionsBuilder
    {
        public static ConnectionString ConnectionString { get; private set; }
        public static DataAccessOptions DataAccessOptions { get; private set; }

        public static void SetConnectionString(ConnectionString connectionString)
        {
            if ( connectionString == null ) throw new ArgumentNullException(nameof(connectionString), $"{nameof(connectionString)} cannot be null.");

            ConnectionString = connectionString;
        }

        public static void SetDataAccessOptions(DataAccessOptions dataAccessOptions)
        {
            if ( dataAccessOptions == null ) throw new ArgumentNullException(nameof(dataAccessOptions), $"{nameof(dataAccessOptions)} cannot be null.");

            DataAccessOptions = dataAccessOptions;
        }
    }
}
