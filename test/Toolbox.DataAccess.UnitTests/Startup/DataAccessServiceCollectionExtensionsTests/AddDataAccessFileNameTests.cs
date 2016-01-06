using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Toolbox.DataAccess.Context;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Uow;
using Xunit;

namespace Toolbox.DataAccess.UnitTests.Startup.DataAccessServiceCollectionExtensionsTests
{
    public class AddDataAccessFileNameTests
    {
        [Fact]
        private void ActionNullRaisesArgumentException()
        {
            Action<DataAccessJsonFile> nullAction = null;
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(nullAction));

            Assert.Equal("setupAction", ex.ParamName);
        }

        [Fact]
        private void UowProviderIsRegisteredAsTransient()
        {
            var connString = new ConnectionString("host", 123, "dbname");
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt => opt.FileName = "_TestData/dbconfig.json");

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

            services.AddDataAccess<EntityContextBase>(opt => opt.FileName = "_TestData/dbconfig.json");

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

            services.AddDataAccess<EntityContextBase>(opt => opt.FileName = "_TestData/dbconfig.json");

            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void FileNameNullRaisesArgumentNullException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.FileName = null));
            Assert.Equal("FileName", ex.ParamName);
        }

        [Fact]
        private void FileNameEmptyRaisesArgumentException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.FileName = ""));
            Assert.Equal("FileName", ex.ParamName);
        }

        [Fact]
        private void FileNameWhitespaceRaisesArgumentException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.FileName = "   "));
            Assert.Equal("FileName", ex.ParamName);
        }

        [Fact]
        private void SectionNullRaisesArgumentNullException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.Section = null));
            Assert.Equal("Section", ex.ParamName);
        }

        [Fact]
        private void SectionEmptyRaisesArgumentException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.Section = ""));
            Assert.Equal("Section", ex.ParamName);
        }

        [Fact]
        private void SetionWhitespaceRaisesArgumentException()
        {
            var services = new ServiceCollection();

            var ex = Assert.Throws<ArgumentException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.Section = "   "));
            Assert.Equal("Section", ex.ParamName);
        }

        [Fact]
        private void NonExistingFileRaisesFileNotFoundException()
        {
            var services = new ServiceCollection();
            var ex = Assert.Throws<FileNotFoundException>(() => services.AddDataAccess<EntityContextBase>(opt => opt.FileName = "non-existing.json"));
            Assert.EndsWith("non-existing.json", ex.FileName);
        }

        [Fact]
        private void DataAccessOptionsAreRegisteredAsSingleton()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<EntityContextBase>(opt =>
                                                        {
                                                            opt.FileName = "_TestData/dbconfig.json";
                                                            opt.Section = "DataAccess";
                                                        });

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<DataAccessOptions>)).ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);

            var configOptions = registrations[0].ImplementationInstance as IConfigureOptions<DataAccessOptions>;
            Assert.NotNull(configOptions);

            var options = new DataAccessOptions();
            configOptions.Configure(options);
            Assert.NotNull(options.ConnectionString);
            Assert.Equal("testhost", options.ConnectionString.Host);
            Assert.Equal(5555, options.ConnectionString.Port);
            Assert.Equal("testdb", options.ConnectionString.DbName);
            Assert.Equal("testuser", options.ConnectionString.User);
            Assert.Equal("testpwd", options.ConnectionString.Password);
            Assert.True(options.LazyLoadingEnabled);
        }
    }
}
