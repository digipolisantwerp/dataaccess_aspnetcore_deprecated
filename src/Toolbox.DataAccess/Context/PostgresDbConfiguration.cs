using System.Data.Entity;

namespace Toolbox.DataAccess.Context
{
    public class PostgresDbConfiguration : DbConfiguration
    {
        public PostgresDbConfiguration()
        {
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
            SetProviderFactory("Npgsql", Npgsql.NpgsqlFactory.Instance);
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
        }
    }
}