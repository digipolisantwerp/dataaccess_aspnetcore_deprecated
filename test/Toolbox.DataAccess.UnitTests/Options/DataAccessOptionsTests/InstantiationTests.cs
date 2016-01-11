using System;
using Toolbox.DataAccess.Options;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.DataAccessOptionsTests
{
    public class InstantiationTests
    {
        [Fact]
        private void ConnectionStringIsInitialized()
        {
            var options = new DataAccessOptions();
            Assert.NotNull(options.ConnectionString);
        }

        [Fact]
        private void DefaultLazyLoadingEnabledIsSet()
        {
            var options = new DataAccessOptions();
            Assert.Equal(Defaults.EntityContextOptions.LazyLoadingEnabled, options.LazyLoadingEnabled);
        }

        [Fact]
        private void DefaultDbConfigurationIsNull()
        {
            var options = new DataAccessOptions();
            Assert.Null(options.DbConfiguration);
        }
    }
}
