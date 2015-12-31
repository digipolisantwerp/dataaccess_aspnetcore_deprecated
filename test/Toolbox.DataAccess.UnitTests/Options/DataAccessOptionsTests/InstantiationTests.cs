using System;
using Toolbox.DataAccess.Options;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.DataAccessOptionsTests
{
    public class InstantiationTests
    {
        [Fact]
        private void LazyLoadingEnabledIsDefaultedToFalse()
        {
            var options = new DataAccessOptions();
            Assert.False(options.LazyLoadingEnabled);
        }
    }
}
