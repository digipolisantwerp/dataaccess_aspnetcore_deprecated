using System;

namespace Toolbox.DataAccess.Options
{
    class ConfigKeys
    {
        internal class Sections
        {
            internal const string ConnectionString = "ConnectionString";
        }

        internal class ConnectionString
        {
            internal const string Host = "Host";
            internal const string Port = "Port";
            internal const string DbName = "DbName";
            internal const string User = "User";
            internal const string Password = "Password";
        }
    }
}
