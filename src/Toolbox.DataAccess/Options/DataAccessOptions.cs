using System;

namespace Toolbox.DataAccess.Options
{
    public class DataAccessOptions 
    {
        public DataAccessOptions(ConnectionString connectionString)
        {
            if ( connectionString == null ) throw new ArgumentNullException(nameof(connectionString), $"{nameof(connectionString)} is null.");
            ConnectionString = connectionString;
        }
        
        public ConnectionString ConnectionString { get; private set; }
    }
}
