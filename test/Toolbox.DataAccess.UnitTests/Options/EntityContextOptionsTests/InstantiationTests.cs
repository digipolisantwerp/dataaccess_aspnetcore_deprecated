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
        private void LazyLoadingEnabledIsDefaultedToFalse()
        {
            var options = new EntityContextOptions();
            Assert.False(options.LazyLoadingEnabled);
        }
    }
}
