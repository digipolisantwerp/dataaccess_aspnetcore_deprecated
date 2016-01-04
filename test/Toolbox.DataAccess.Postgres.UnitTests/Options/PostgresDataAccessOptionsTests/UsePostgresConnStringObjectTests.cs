using System;
using Toolbox.DataAccess.Postgres.Options;
using Xunit;

namespace Toolbox.DataAccess.Postgres.UnitTests.Options.PostgresDataAccessOptionsTests
{
    public class UsePostgresConnStringObjectTests
    {
        [Fact]
        public void ConnectionStringNullRaisesArgumentNullException()
        {
            ConnectionString nullConnString = null;
            var options = new PostgresDataAccessOptions();

            var ex = Assert.Throws<ArgumentNullException>(() => options.UsePostgres(nullConnString));

            Assert.Equal("connectionString", ex.ParamName);
        }
    }
}
