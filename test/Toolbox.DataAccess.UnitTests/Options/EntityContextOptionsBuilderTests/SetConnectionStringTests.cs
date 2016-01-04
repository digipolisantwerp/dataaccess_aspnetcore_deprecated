using System;
using Toolbox.DataAccess.Options;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.EntityContextOptionsBuilderTests
{
    public class SetConnectionStringTests
    {
        [Fact]
        private void NullRaisesArgumentNullException()
        {
            ConnectionString nullConnectionString = null;
            var ex = Assert.Throws<ArgumentNullException>(() => EntityContextOptionsBuilder.SetConnectionString(nullConnectionString));
            Assert.Equal("connectionString", ex.ParamName);
        }

        [Fact]
        private void ConnectionStringIsSet()
        {
            var connectionString = new ConnectionString("host", 0, "dbname");
            EntityContextOptionsBuilder.SetConnectionString(connectionString);
            Assert.Same(connectionString, EntityContextOptionsBuilder.ConnectionString);
        }
    }
}
