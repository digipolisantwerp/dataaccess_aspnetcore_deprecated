using System;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.Postgres
{
    public class PostgresConnectionString : ConnectionString
    {
        public PostgresConnectionString(string host, ushort port, string dbname, string user, string password) : base(host, port, dbname, user, password)
        { }

        public override string ToString()
        {
            var result = "";
            if ( Port == 0 )
                result = string.Format("Server={0};Database={1};User Id={2};Password={3}", Host, DbName, User, Password);
            else
                result = string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4}", Host, Port, DbName, User, Password);
            return result;
        }
    }
}
