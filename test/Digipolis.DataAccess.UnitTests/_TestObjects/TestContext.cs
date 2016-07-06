using Digipolis.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.DataAccess.UnitTests
{
    public class TestContext : EntityContextBase<TestContext>
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        { }
    }
}
