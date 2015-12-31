using System;

namespace Toolbox.DataAccess.Options
{
    public class EntityContextOptions
    {
        public ConnectionString ConnectionString { get; set; }
        public bool LazyLoadingEnabled { get; set; }
    }
}
