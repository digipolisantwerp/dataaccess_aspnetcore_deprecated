//using System;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Toolbox.DataAccess.Options;
//using Toolbox.DataAccess.Entiteiten;
//using Toolbox.DataAccess.Uow;
//using Toolbox.DataAccess.Repositories;

//namespace Toolbox.DataAccess.StartupExtensions
//{
//    public static class DataAccessServiceCollectionExtentions
//    {
//        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<DataAccessOptions> setupAction) where TEntityContext : EntityContextBase
//        {
//            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} is null.");

//            //RegisterDataAccess<TEntityContext>(services, options);

//            return services;
//        }

//        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, DataAccessOptions options) where TEntityContext : EntityContextBase
//        {
//            if ( options == null ) throw new ArgumentNullException(nameof(options), $"{nameof(options)} is null.");
            
//            RegisterDataAccess<TEntityContext>(services, options);
            
//            return services;
//        }
        
//        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, string optionsFilePath) where TEntityContext : EntityContextBase
//        {
//            if ( optionsFilePath == null ) throw new ArgumentNullException(nameof(optionsFilePath), $"{nameof(optionsFilePath)} is null.");

//            var connectionString = ReadConnectionString(optionsFilePath);
//            var dataAccessOptions = new DataAccessOptions(connectionString);

//            AddDataAccess<TEntityContext>(services, dataAccessOptions);

//            return services;
//        }

//        private static ConnectionString ReadConnectionString(string jsonFilename)
//        {
//            var builder = new ConfigurationBuilder();
//            builder.AddJsonFile(jsonFilename);
//            var config = builder.Build();

//            //var host = config.Get(ConfigKeys.ConnectionString.Host);
//            //var port = config.Get(ConfigKeys.ConnectionString.Port);
//            //var dbname = config.Get(ConfigKeys.ConnectionString.DbName);
//            //var user = config.Get(ConfigKeys.ConnectionString.User);
//            //var pwd = config.Get(ConfigKeys.ConnectionString.Password);

//            //return new ConnectionString(host, port, dbname, user, pwd);

//            return null;
//        }

//        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services, DataAccessOptions options) where TEntityContext : EntityContextBase
//        {

//            services.AddInstance(options);
//            //services.Configure<DataAccessOptions>(opt => opt.SetConnectionString(options.ConnectionString); );      // ToDo (SVB) : bovenstaande lijn vervangen en testen


//            services.TryAddTransient<IUowProvider, UowProvider>();
//            services.TryAddTransient<EntityContextBase, TEntityContext>();
//            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
//        }
//    }
//}
