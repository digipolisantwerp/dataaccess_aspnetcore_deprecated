using System.Data.Entity;
using Microsoft.AspNet.Builder;
using Toolbox.DataAccess.Context;

namespace Toolbox.DataAccess.StartupExtensions
{
    public static class DataAccessAppBuilderExtensions
    {
        public static IApplicationBuilder UseDataAccess(this IApplicationBuilder app, DbConfiguration dbConfiguration = null)
        {
            if (dbConfiguration == null) dbConfiguration = new PostgresDbConfiguration();
            DbConfiguration.SetConfiguration(dbConfiguration);

            return app;
        }
    }
}
