using System;
using Toolbox.DataAccess.Options;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.ConnectionStringTests
{
    public class ToStringTests
    {
        [Fact]
        private void StringContainsHost()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Server=host;", conn.ToString());
        }

        [Fact]
        private void StringDoesNotContainPortZero()
        {
            var conn = new ConnectionString("host", 0, "db", "user", "pwd");
            Assert.DoesNotContain("Port=0", conn.ToString());
        }

        [Fact]
        private void StringContainsPort()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Port=1234;", conn.ToString());
        }

        [Fact]
        private void StringContainsDbName()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Database=db;", conn.ToString());
        }

        [Fact]
        private void StringContainsUser()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("User Id=user;", conn.ToString());
        }

        [Fact]
        private void StringContainsPassword()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Password=pwd", conn.ToString());
        }
    }
}
