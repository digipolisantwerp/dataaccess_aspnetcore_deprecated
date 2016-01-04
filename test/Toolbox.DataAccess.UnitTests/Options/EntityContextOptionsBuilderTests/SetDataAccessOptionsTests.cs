using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.DataAccess.Options;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Options.EntityContextOptionsBuilderTests
{
    public class SetDataAccessOptionsTests
    {
        [Fact]
        private void NullRaisesArgumentNullException()
        {
            DataAccessOptions nullOptions = null;
            var ex = Assert.Throws<ArgumentNullException>(() => EntityContextOptionsBuilder.SetDataAccessOptions(nullOptions));
            Assert.Equal("dataAccessOptions", ex.ParamName);
        }

        [Fact]
        private void DataAccessOptionsIsSet()
        {
            var options = new DataAccessOptions();
            EntityContextOptionsBuilder.SetDataAccessOptions(options);
            Assert.Same(options, EntityContextOptionsBuilder.DataAccessOptions);
        }
    }
}
