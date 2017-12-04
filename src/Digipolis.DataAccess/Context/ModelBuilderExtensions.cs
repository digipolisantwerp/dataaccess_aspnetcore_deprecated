using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Digipolis.DataAccess.Context
{
    public static class ModelBuilderExtensions
    {
        public static void LowerCaseTablesAndFields(this ModelBuilder modelBuilder)
        {
            foreach ( var entityType in modelBuilder.Model.GetEntityTypes() )
            {
                // Skip shadow types
                if ( entityType.ClrType == null )
                {
                    continue;
                }

                // Set the table name to the (simple) name of the CLR type and lowercase it
                entityType.Relational().TableName = entityType.ClrType.Name.ToLower();

                // Lowercase all properties
                foreach ( var property in entityType.GetProperties() )
                {
                    //Check if property has a ColumnAttribute. If so, use the lowercased value of this attribute instead of lowercased property name
                    var columnNameAttribute = property.GetAnnotations().FirstOrDefault(x => x.Name.ToLower().Contains("columnname"))?.Value;
                    property.Relational().ColumnName = columnNameAttribute == null ? property.Name.ToLower() : columnNameAttribute.ToString().ToLower();
                }
            }
        }

        public static void DisableCascadingDeletes(this ModelBuilder modelBuilder)
        {
            foreach ( var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()) )
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
