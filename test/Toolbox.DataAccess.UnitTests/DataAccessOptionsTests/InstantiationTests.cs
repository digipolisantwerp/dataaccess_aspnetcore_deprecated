using System;
using Microsoft.Extensions.DependencyInjection;
using Toolbox.DataAccess.Entiteiten;
using Toolbox.DataAccess.Options;
using Toolbox.DataAccess.StartupExtensions;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.DataAccessOptionsTests
{
    public class InstantiationTests
    {
        [Fact]
        private void ConnectionStringNullRaisesArgumentNullException()
        {
            ConnectionString nullConnString = null;
            var ex = Assert.Throws<ArgumentNullException>(() => new DataAccessOptions(nullConnString));
            Assert.Equal("connectionString", ex.ParamName);
        }

        [Fact]
        private void ConnectionStringIsSet()
        {
            var connectionString = new TestConnectionString("host", 1234, "db", "user", "pwd");
            var options = new DataAccessOptions(connectionString);
            Assert.Same(connectionString, options.ConnectionString);
        }
    }
}
