using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Toolbox.DataAccess.Context;
using Toolbox.DataAccess.Options;
using Toolbox.DataAccess.Paging;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Uow;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Startup.ServiceCollectionExtensionsTests
{
    public class AddDataAccessOptionsTests
    {
        [Fact]
        private void ActionNullRaisesArgumentException()
        {
            Action<DataAccessOptions> nullAction = null;
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(nullAction));

            Assert.Equal("setupAction", ex.ParamName);
        }

        [Fact]
        private void ConnectionStringNullRaisesArgumentNullException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.ConnectionString = null));

            Assert.Equal("ConnectionString", ex.ParamName);
        }

        [Fact]
        private void UowProviderIsRegisteredAsSingleton()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt => opt.ConnectionString = connString);

            var registrations = services.Where(sd => sd.ServiceType == typeof(IUowProvider)
                                               && sd.ImplementationType == typeof(UowProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

        [Fact]
        private void EntityContextIsRegisteredAsTransient()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt => opt.ConnectionString = connString);

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

            services.AddDataAccess<EntityContextBase>(opt => opt.ConnectionString = connString);

            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void EntityContextOptionsIsRegisteredAsSingleton()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt => 
                                                        {
                                                            opt.ConnectionString = connString;
                                                            opt.LazyLoadingEnabled = true;
                                                        } );

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<EntityContextOptions>)).ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);

            var configOptions = registrations[0].ImplementationInstance as IConfigureOptions<EntityContextOptions>;
            Assert.NotNull(configOptions);

            var options = new EntityContextOptions();
            configOptions.Configure(options);
            Assert.Same(connString, options.ConnectionString);
            Assert.True(options.LazyLoadingEnabled);
        }

        [Fact]
        private void OptionalEntityContextOptionsAreSet()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt =>
            {
                opt.ConnectionString = connString;
                opt.LazyLoadingEnabled = true;
                opt.DefaultSchema = "schemaname";
                opt.PluralizeTableNames = false;
            });

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<EntityContextOptions>)).ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);

            var configOptions = registrations[0].ImplementationInstance as IConfigureOptions<EntityContextOptions>;
            Assert.NotNull(configOptions);

            var options = new EntityContextOptions();
            configOptions.Configure(options);
            Assert.False(options.PluralizeTableNames);
            Assert.Equal("schemaname", options.DefaultSchema);
        }

        [Fact]
        private void DataPagerIsRegisteredAsTransient()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt => opt.ConnectionString = connString);

            var registrations = services.Where(sd => sd.ServiceType == typeof(IDataPager<>)
                                               && sd.ImplementationType == typeof(DataPager<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void DbConfigurationIsLoaded()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var dbConfig = new TestDbConfiguration();
            var services = new ServiceCollection();

            TestDbConfiguration registeredDbConfig = null;

            DbConfiguration.Loaded += (sender, e) => registeredDbConfig = sender as TestDbConfiguration;

            services.AddDataAccess<EntityContextBase>(opt =>
                                                        {
                                                            opt.ConnectionString = connString;
                                                            opt.DbConfiguration = dbConfig;
                                                        });

            Assert.NotNull(registeredDbConfig);
        }
    }
}
