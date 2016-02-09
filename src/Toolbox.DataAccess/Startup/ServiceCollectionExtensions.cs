using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Toolbox.DataAccess.Context;
using Toolbox.DataAccess.Uow;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Options;
using System.Data.Entity;
using Toolbox.DataAccess.Paging;

namespace Toolbox.DataAccess
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<DataAccessOptions> setupAction) where TEntityContext : EntityContextBase
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var options = new DataAccessOptions();
            setupAction.Invoke(options);

            ConfigureEntityContextOptions(services, options);
            ConfigureDbConfiguration(options.DbConfiguration);

            RegisterDataAccess<TEntityContext>(services);

            return services;
        }

        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<DataAccessJsonFile> setupAction) where TEntityContext : EntityContextBase
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var options = new DataAccessJsonFile();
            setupAction.Invoke(options);
            
            ConfigureEntityContextOptions(services, options);
            ConfigureDbConfiguration(options.DbConfiguration);

            RegisterDataAccess<TEntityContext>(services);

            return services;
        }

        private static void ConfigureDbConfiguration(DbConfiguration dbConfig)
        {
            if ( dbConfig != null ) DbConfiguration.SetConfiguration(dbConfig);
        }

        private static void ConfigureEntityContextOptions(IServiceCollection services, DataAccessOptions options)
        {
            ValidateMandatoryField(options.ConnectionString, nameof(options.ConnectionString));

            services.Configure<EntityContextOptions>(opt =>
            {
                opt.ConnectionString = options.ConnectionString;
                opt.LazyLoadingEnabled = options.LazyLoadingEnabled;
                opt.DefaultSchema = options.DefaultSchema;
                opt.PluralizeTableNames = options.PluralizeTableNames;
            });
        }

        private static void ConfigureEntityContextOptions(IServiceCollection services, DataAccessJsonFile options)
        {
            ValidateMandatoryField(options.FileName, nameof(options.FileName));
            ValidateMandatoryField(options.Section, nameof(options.Section));

            var builder = new ConfigurationBuilder().AddJsonFile(options.FileName);
            var config = builder.Build();
            var section = config.GetSection(options.Section);
            services.Configure<EntityContextOptions>(section);
        }

        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services) where TEntityContext : EntityContextBase
        {
            services.TryAddSingleton<IUowProvider, UowProvider>();
            services.TryAddTransient<EntityContextBase, TEntityContext>();
            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
            services.TryAddTransient(typeof(IDataPager<>), typeof(DataPager<>));
        }

        private static void ValidateMandatoryField(string field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
            if ( field.Trim() == String.Empty ) throw new ArgumentException($"{fieldName} cannot be empty.", fieldName);
        }

        private static void ValidateMandatoryField(object field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
        }
    }
}
