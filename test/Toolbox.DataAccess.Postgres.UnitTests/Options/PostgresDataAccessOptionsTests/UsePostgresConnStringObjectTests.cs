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

        [Fact]
        private void ConnectionStringIsSet()
        {
            var connString = new ConnectionString("host", 1234, "dbname");
            var options = new PostgresDataAccessOptions();

            options.UsePostgres(connString);

            Assert.Same(connString, options.ConnectionString);
        }
    }
}
