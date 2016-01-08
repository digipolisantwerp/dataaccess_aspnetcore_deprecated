using System;
using System.Data.Entity;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess
{
    public class DataAccessOptions : EntityContextOptions
    {
        public DbConfiguration DbConfiguration { get; set; }
    }
}
