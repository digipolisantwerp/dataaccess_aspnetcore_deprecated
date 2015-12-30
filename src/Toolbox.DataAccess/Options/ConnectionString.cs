using System;

namespace Toolbox.DataAccess.Options
{
    public abstract class ConnectionString
    {
        protected ConnectionString(string host, ushort port, string dbname, string user, string password)
        {
            ValidateArguments(host, dbname, user, password);
            Host = host;
            Port = port;
            DbName = dbname;
            User = user;
            Password = password;
        }

        public string Host { get; private set; }
        public ushort Port { get; private set; }
        public string DbName { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }

        private void ValidateArguments(string host, string dbname, string user, string password)
        {
            if ( host == null ) throw new ArgumentNullException(nameof(host), $"{nameof(host)} is null.");
            if ( dbname == null ) throw new ArgumentNullException(nameof(dbname), $"{nameof(dbname)} is null.");
            if ( user == null ) throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");
            if ( password == null ) throw new ArgumentNullException(nameof(password), $"{nameof(password)} is null.");

            if ( host.Trim() == String.Empty ) throw new ArgumentException($"{nameof(host)} is empty.", nameof(host));
            if ( dbname.Trim() == String.Empty ) throw new ArgumentException($"{nameof(dbname)} is empty.", nameof(dbname));
            if ( user.Trim() == String.Empty ) throw new ArgumentException($"{nameof(user)} is empty.", nameof(user));
            if ( password.Trim() == String.Empty ) throw new ArgumentException($"{nameof(password)} is empty.", nameof(password));
        }
    }
}
