//using System;
//using System.Linq;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.OptionsModel;
//using Toolbox.DataAccess.Entiteiten;
//using Toolbox.DataAccess.Options;
//using Toolbox.DataAccess.Repositories;
//using Toolbox.DataAccess.StartupExtensions;
//using Toolbox.DataAccess.Uow;
//using Xunit;

//namespace Toolbox.DataAccess.UnitTests.Startup.DataAccessServiceCollectionExtensionsTests
//{
//    public class AddDataAccessTests
//    {
//        [Fact]
//        private void PostgresDataAccessOptionsActionNullRaisesArgumentException()
//        {
//            Action<DataAccessOptions> nullAction = null;
//            var services = new ServiceCollection();

//            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(nullAction));

//            Assert.Equal("setupAction", ex.ParamName);
//        }

//        [Fact]
//        private void UowProviderIsRegisteredAsTransient()
//        {
//            var connString = new ConnectionString("host", 123, "dbname");
//            var services = new ServiceCollection();

//            services.AddDataAccess<EntityContextBase>(options => options.ConnectionString = connString);

//            var registrations = services.Where(sd => sd.ServiceType == typeof(IUowProvider)
//                                               && sd.ImplementationType == typeof(UowProvider))
//                                        .ToArray();
//            Assert.Equal(1, registrations.Count());
//            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
//        }

//        [Fact]
//        private void EntityContextIsRegisteredAsTransient()
//        {
//            var connString = new ConnectionString("host", 123, "dbname");
//            var services = new ServiceCollection();

//            services.AddDataAccess<EntityContextBase>(options => options.ConnectionString = connString);

//            var registrations = services.Where(sd => sd.ServiceType == typeof(EntityContextBase)
//                                               && sd.ImplementationType == typeof(EntityContextBase))
//                                        .ToArray();
//            Assert.Equal(1, registrations.Count());
//            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
//        }

//        [Fact]
//        private void GenericRepositoryIsRegisteredAsTransient()
//        {
//            var connString = new ConnectionString("host", 123, "dbname");
//            var services = new ServiceCollection();

//            services.AddDataAccess<EntityContextBase>(options => options.ConnectionString = connString);

//            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
//                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
//                                        .ToArray();
//            Assert.Equal(1, registrations.Count());
//            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
//        }

//        [Fact]
//        private void EntityContextOptionsAreRegistered()
//        {
//            var connString = new ConnectionString("host", 123, "dbname");
//            var services = new ServiceCollection();
//            services.AddOptions();

//            services.AddDataAccess<EntityContextBase>(options => options.ConnectionString = connString);

//            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<EntityContextOptions>)).ToArray();
//            Assert.Equal(1, registrations.Count());
//            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
//        }
//    }
//}
