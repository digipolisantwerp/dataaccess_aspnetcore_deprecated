using Digipolis.DataAccess.Options;
using System;
using System.Reflection;

namespace Digipolis.DataAccess
{
    public class ConnectionString
    {
        public ConnectionString() : this(Defaults.ConnectionString.Host, Defaults.ConnectionString.Port, Assembly.GetEntryAssembly().GetName().Name)
        { }

        public ConnectionString(string host, ushort port, string dbname, string user = null, string password = null)
        {
            ValidateArguments(host, dbname);
            Host = host;
            Port = port;
            DbName = dbname;
            User = user;
            Password = password;
        }

        public string Host { get; set; }
        public ushort Port { get; set; }
        public string DbName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        private void ValidateArguments(string host, string dbname)
        {
            if ( host == null ) throw new ArgumentNullException(nameof(host), $"{nameof(host)} is null.");
            if ( dbname == null ) throw new ArgumentNullException(nameof(dbname), $"{nameof(dbname)} is null.");

            if ( host.Trim() == String.Empty ) throw new ArgumentException($"{nameof(host)} is empty.", nameof(host));
            if ( dbname.Trim() == String.Empty ) throw new ArgumentException($"{nameof(dbname)} is empty.", nameof(dbname));
        }

        public override string ToString()
        {
            var result = $"Server={Host};Database={DbName};";

            if ( Port > 0 ) result += $"Port={Port};";

            if ( User == null )
                result += $"Integrated Security=true;";
            else
            {
                result += $"User Id={User};";
                if ( Password != null ) result += $"Password={Password};";
            }

            return result;
        }
    }
}
