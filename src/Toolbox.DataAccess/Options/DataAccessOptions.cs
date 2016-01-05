using System;

namespace Toolbox.DataAccess
{
    public class DataAccessOptions 
    {
        public ConnectionString ConnectionString { get; set; } = new ConnectionString();

        public bool LazyLoadingEnabled { get; set; } = false;
    }
}
