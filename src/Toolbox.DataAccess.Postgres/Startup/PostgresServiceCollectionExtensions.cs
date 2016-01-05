//using System;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Toolbox.DataAccess.Entiteiten;
//using Toolbox.DataAccess.Options;
//using Toolbox.DataAccess.Postgres.Options;
//using Toolbox.DataAccess.Repositories;
//using Toolbox.DataAccess.Uow;

//namespace Toolbox.DataAccess
//{
//    public static class PostgresServiceCollectionExtensions
//    {
//        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<PostgresDataAccessOptions> setupAction) where TEntityContext : EntityContextBase
//        {
//            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

//            // ToDo (SVB) : register options
//            //var options = new PostgresDataAccessOptions();
//            //setupAction.Invoke(options);
//            //services.Configure<EntityContextOptions>(opt => opt.ConnectionString = options.ConnectionString);      // ToDo (SVB) : bovenstaande lijn vervangen en testen

//            services.Configure(setupAction);

//            RegisterDataAccess<TEntityContext>(services);

//            // ToDo (SVB) : register postgres specific stuff

//            return services;
//        }

//        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services) where TEntityContext : EntityContextBase
//        {
//            services.TryAddTransient<IUowProvider, UowProvider>();
//            services.TryAddTransient<EntityContextBase, TEntityContext>();
//            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
//        }
//    }
//}
