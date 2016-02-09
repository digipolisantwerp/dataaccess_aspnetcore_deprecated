using System;
using System.Reflection;

namespace Toolbox.DataAccess.Options
{
    public class Defaults
    {
        public class ConnectionString
        {
            public const string Host = "localhost";
            public const int Port = 0;
        }

        public class DataAccessJsonFile
        {
            public const string FileName = "dbconfig.json";
            public const string Section = "DataAccess";
        }

        public class EntityContextOptions
        {
            public const bool LazyLoadingEnabled = false;
            public const bool PluralizeTableNames = true;
            public const string DefaultSchema = "dbo";
        }
    }
}
