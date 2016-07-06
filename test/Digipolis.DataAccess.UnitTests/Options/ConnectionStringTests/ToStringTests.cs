using Digipolis.DataAccess;
using System;
using Xunit;

namespace Digipolis.DataAccess.UnitTests.Options.ConnectionStringTests
{
    public class ToStringTests
    {
        [Fact]
        private void HostIsAssignedToServer()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Server=host;", conn.ToString());
        }

        [Fact]
        private void PortZeroIsNotAssigned()
        {
            var conn = new ConnectionString("host", 0, "db", "user", "pwd");
            Assert.DoesNotContain("Port=0", conn.ToString());
        }

        [Fact]
        private void PortIsAssignedToPort()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Port=1234;", conn.ToString());
        }

        [Fact]
        private void DbNameIsAssignedToDatabase()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Database=db;", conn.ToString());
        }

        [Fact]
        private void UserIsAssignedToUserId()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("User Id=user;", conn.ToString());
            Assert.DoesNotContain("Integrated Security", conn.ToString());
        }

        [Fact]
        private void NullUserSetsIntegratedSecurity()
        {
            var conn = new ConnectionString("host", 1234, "db", null, "pwd");
            Assert.Contains("Integrated Security=true;", conn.ToString());
            Assert.DoesNotContain("User Id", conn.ToString());
            Assert.DoesNotContain("Password", conn.ToString());
        }

        [Fact]
        private void PasswordIsAssignedToPassword()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", "pwd");
            Assert.Contains("Password=pwd;", conn.ToString());
        }

        [Fact]
        private void NullPasswordIsNotAssigned()
        {
            var conn = new ConnectionString("host", 1234, "db", "user", null);
            Assert.DoesNotContain("Password", conn.ToString());
        }
    }
}
