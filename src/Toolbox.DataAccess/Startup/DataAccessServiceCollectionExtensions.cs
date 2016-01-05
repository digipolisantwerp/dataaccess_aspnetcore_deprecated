using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Toolbox.DataAccess.Context;
using Toolbox.DataAccess.Uow;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Exceptions;

namespace Toolbox.DataAccess
{
    public static class DataAccessServiceCollectionExtentions
    {
        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<DataAccessOptions> setupAction) where TEntityContext : EntityContextBase
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            services.Configure(setupAction);

            RegisterDataAccess<TEntityContext>(services);

            return services;
        }

        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<DataAccessJsonFile> setupAction) where TEntityContext : EntityContextBase
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var options = new DataAccessJsonFile();
            setupAction.Invoke(options);

            ValidateMandatoryField(options.FileName, nameof(options.FileName));
            ValidateMandatoryField(options.Section, nameof(options.Section));

            ConfigureOptions(services, options);
            RegisterDataAccess<TEntityContext>(services);

            return services;
        }

        private static void ConfigureOptions(IServiceCollection services, DataAccessJsonFile options)
        {
            var builder = new ConfigurationBuilder().AddJsonFile(options.FileName);
            var config = builder.Build();
            var section = config.GetSection(options.Section);
            services.Configure<DataAccessOptions>(section);
        }

        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services) where TEntityContext : EntityContextBase
        {
            services.TryAddTransient<IUowProvider, UowProvider>();
            services.TryAddTransient<EntityContextBase, TEntityContext>();
            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
        }

        private static void ValidateMandatoryField(string field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
            if ( field.Trim() == String.Empty ) throw new ArgumentException($"{fieldName} cannot be empty.", fieldName);
        }
    }
}
