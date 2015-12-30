using System;
using Toolbox.DataAccess.Options;

namespace Toolbox.DataAccess.UnitTests
{
    public class TestConnectionString : ConnectionString
    {
        public TestConnectionString(string host, ushort port, string dbname, string user, string password) : base(host, port, dbname, user, password)
        { }
    }
}