using System;
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
        private void LazyLoadingEnabledIsDefaultedToFalse()
        {
            var options = new DataAccessOptions();
            Assert.False(options.LazyLoadingEnabled);
        }
    }
}
