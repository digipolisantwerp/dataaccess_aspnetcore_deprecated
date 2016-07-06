using System;
using System.Reflection;
using Digipolis.DataAccess.Options;
using Xunit;
using Digipolis.DataAccess;

namespace Digipolis.DataAccess.UnitTests.Options.ConnectionStringTests
{
    public class InstantiationTests
    {
        [Fact]
        private void HostNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionString(null, 1234, "db", "user", "pwd"));
            Assert.Equal("host", ex.ParamName);
        }

        [Fact]
        private void HostEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new ConnectionString("", 1234, "db", "user", "pwd"));
            Assert.Equal("host", ex.ParamName);
        }

        [Fact]
        private void HostWhiteSpaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new ConnectionString("  ", 1234, "db", "user", "pwd"));
            Assert.Equal("host", ex.ParamName);
        }

        [Fact]
        private void DbNameNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionString("host", 1234, null, "user", "pwd"));
            Assert.Equal("dbname", ex.ParamName);
        }

        [Fact]
        private void DbNameEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new ConnectionString("host", 1234, "", "user", "pwd"));
            Assert.Equal("dbname", ex.ParamName);
        }

        [Fact]
        private void DbNameWhiteSpaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new ConnectionString("host", 1234, "   ", "user", "pwd"));
            Assert.Equal("dbname", ex.ParamName);
        }

        [Fact]
        private void HostIsSet()
        {
            var conn =  new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Equal("host", conn.Host);
        }

        [Fact]
        private void PortIsSet()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Equal(1234, conn.Port);
        }

        [Fact]
        private void PortZeroIsAllowed()
        {
            var conn = new ConnectionString("host", 0, "db", "user", "pwd");
            Assert.Equal(0, conn.Port);
        }

        [Fact]
        private void DbNameIsSet()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Equal("db", conn.DbName);
        }

        [Fact]
        private void UserIsSet()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Equal("user", conn.User);
        }

        [Fact]
        private void UserNullIsAllowed()
        {
            var conn = new ConnectionString("host", 1234, "db", null, "pwd");
            Assert.Null(conn.User);
        }

        [Fact]
        private void PasswordIsSet()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Equal("pwd", conn.Password);
        }

        [Fact]
        private void PasswordNullIsAllowed()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", null);
            Assert.Null(conn.Password);
        }

        [Fact]
        private void DefaultHostIsLocalhost()
        {
            var conn = new ConnectionString();
            Assert.Equal(Defaults.ConnectionString.Host, conn.Host);
        }

        [Fact]
        private void DefaultPortIsZero()
        {
            var conn = new ConnectionString();
            Assert.Equal(Defaults.ConnectionString.Port, conn.Port);
        }

        [Fact]
        private void DefaultDbNameIsCurrentAssembly()
        {
            var conn = new ConnectionString();
            Assert.Equal(Assembly.GetEntryAssembly().GetName().Name, conn.DbName);
        }

        [Fact]
        private void DefaultUserIsNull()
        {
            var conn = new ConnectionString();
            Assert.Null(conn.User);
        }

        [Fact]
        private void DefaultPasswordIsNull()
        {
            var conn = new ConnectionString();
            Assert.Null(conn.Password);
        }
    }
}
