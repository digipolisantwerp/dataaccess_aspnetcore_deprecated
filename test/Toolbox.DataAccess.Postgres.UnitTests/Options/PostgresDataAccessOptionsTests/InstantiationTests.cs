//using System;
//using Toolbox.DataAccess.Postgres.Options;
//using Xunit;

//namespace Toolbox.DataAccess.Postgres.UnitTests.Options.PostgresDataAccessOptionsTests
//{
//    public class InstantiationTests
//    {
//        [Fact]
//        private void ConnectionStringIsDefaulted()
//        {
//            var options = new PostgresDataAccessOptions();
//            Assert.NotNull(options.ConnectionString);
//            Assert.Equal("localhost", options.ConnectionString.Host);
//            Assert.Equal(5432, options.ConnectionString.Port);
//            Assert.NotNull(options.ConnectionString.DbName);
//            Assert.Equal("postgres", options.ConnectionString.User);
//            Assert.Equal("postgres", options.ConnectionString.Password);
//        }

//        [Fact]
//        private void DataAccessOptionsIsInstantiated()
//        {
//            var options = new PostgresDataAccessOptions();
//            Assert.NotNull(options.DataAccessOptions);
//        }
//    }
//}
