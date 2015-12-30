using System;
using Toolbox.DataAccess.Postgres;
using Xunit;

namespace Toolbox.DataAccess.UnitTestsPostgresConnectionStringTests
{
    public class ToStringTests
    {
        [Fact]
        private void StringContainsHost()
        {
            var conn = new PostgresConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Server=host;", conn.ToString());
        }

        [Fact]
        private void StringDoesNotContainPortZero()
        {
            var conn = new PostgresConnectionString("host", 0, "db", "user", "pwd");
            Assert.DoesNotContain("Port=0", conn.ToString());
        }

        [Fact]
        private void StringContainsPort()
        {
            var conn = new PostgresConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Port=1234;", conn.ToString());
        }

        [Fact]
        private void StringContainsDbName()
        {
            var conn = new PostgresConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Database=db;", conn.ToString());
        }

        [Fact]
        private void StringContainsUser()
        {
            var conn = new PostgresConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("User Id=user;", conn.ToString());
        }

        [Fact]
        private void StringContainsPassword()
        {
            var conn = new PostgresConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Password=pwd", conn.ToString());
        }
    }
}
