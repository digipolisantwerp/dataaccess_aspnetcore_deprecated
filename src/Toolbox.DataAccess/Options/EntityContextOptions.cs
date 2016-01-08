using System;

namespace Toolbox.DataAccess.Options
{
    public class EntityContextOptions
    {
        public ConnectionString ConnectionString { get; set; } = new ConnectionString();

        public bool LazyLoadingEnabled { get; set; } = false;
    }
}
