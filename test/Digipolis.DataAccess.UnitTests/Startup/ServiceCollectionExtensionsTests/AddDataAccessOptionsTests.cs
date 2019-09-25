using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Digipolis.DataAccess.Context;
using Digipolis.DataAccess.Options;
using Digipolis.DataAccess.Paging;
using Digipolis.DataAccess.Repositories;
using Digipolis.DataAccess.Uow;
using Xunit;
using Digipolis.DataAccess;

namespace Digipolis.DataAccess.UnitTests.Startup.ServiceCollectionExtensionsTests
{
    public class AddDataAccessOptionsTests
    {
        [Fact]
        private void UowProviderIsRegisteredAsScoped()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IUowProvider)
                                               && sd.ImplementationType == typeof(UowProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Scoped, registrations[0].Lifetime);
        }

        [Fact]
        private void GenericRepositoryIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void DataPagerIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IDataPager<>)
                                               && sd.ImplementationType == typeof(DataPager<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }
    }
}
