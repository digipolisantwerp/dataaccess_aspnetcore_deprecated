using System;
using Toolbox.DataAccess.Options;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.EntityContextOptionsTests
{
    public class InstantiationTests
    {
        [Fact]
        private void ConnectionStringIsInitialized()
        {
            var options = new EntityContextOptions();
            Assert.NotNull(options.ConnectionString);
        }

        [Fact]
        private void DefaultLazyLoadingEnabledIsSet()
        {
            var options = new EntityContextOptions();
            Assert.Equal(Defaults.EntityContextOptions.LazyLoadingEnabled, options.LazyLoadingEnabled);
        }

        [Fact]
        private void DefaultPluralizeTableNamesIsSet()
        {
            var options = new EntityContextOptions();
            Assert.Equal(Defaults.EntityContextOptions.PluralizeTableNames, options.PluralizeTableNames);
        }

        [Fact]
        private void DefaultDefaultSchemaIsSet()
        {
            var options = new EntityContextOptions();
            Assert.Equal(Defaults.EntityContextOptions.DefaultSchema, options.DefaultSchema);
        }

    }
}
