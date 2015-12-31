using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Toolbox.DataAccess.Entiteiten;
using Toolbox.DataAccess.Postgres.Options;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Uow;
using Xunit;

namespace Toolbox.DataAccess.Postgres.UnitTests.Startup.PostgresServiceCollectionExtensionsTests
{
    public class AddDataAccessTests
    {
        [Fact]
        private void PostgresDataAccessOptionsActionNullRaisesArgumentException()
        {
            Action<PostgresDataAccessOptions> nullAction = null;
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(nullAction));

            Assert.Equal("setupAction", ex.ParamName);
        }

        [Fact]
        private void UowProviderIsRegisteredAsTransient()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(options => options.UsePostgres(connString));

            var registrations = services.Where(sd => sd.ServiceType == typeof(IUowProvider)
                                               && sd.ImplementationType == typeof(UowProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void EntityContextIsRegisteredAsTransient()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(options => options.UsePostgres(connString));

            var registrations = services.Where(sd => sd.ServiceType == typeof(EntityContextBase)
                                               && sd.ImplementationType == typeof(EntityContextBase))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void GenericRepositoryIsRegisteredAsTransient()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(options => options.UsePostgres(connString));

            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }
    }
}
