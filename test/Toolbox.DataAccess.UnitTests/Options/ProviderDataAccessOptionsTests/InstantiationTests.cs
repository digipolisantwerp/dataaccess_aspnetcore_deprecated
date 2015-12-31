using System;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.ProviderDataAccessOptionsTests
{
    public class InstantiationTests
    {
        [Fact]
        private void DataAccessOptionsIsInstantiated()
        {
            var options = new TestDataAccessOptions();
            Assert.NotNull(options.DataAccessOptions);
        }
    }
}
