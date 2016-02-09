using System.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using Toolbox.DataAccess.Options;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Toolbox.DataAccess.Context
{
    public class EntityContextBase : DbContext
    {
        public EntityContextBase(IOptions<EntityContextOptions> options) : base(options.Value.ConnectionString.ToString())
        {
            EntityContextOptions = options.Value;
            this.Configuration.LazyLoadingEnabled = EntityContextOptions.LazyLoadingEnabled;
        }

        protected EntityContextOptions EntityContextOptions { get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (EntityContextOptions.DefaultSchema != Defaults.EntityContextOptions.DefaultSchema)
                modelBuilder.HasDefaultSchema(EntityContextOptions.DefaultSchema);

            if(!EntityContextOptions.PluralizeTableNames)
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
