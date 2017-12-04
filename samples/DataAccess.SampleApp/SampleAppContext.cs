using DataAccess.SampleApp.Entities;
using Digipolis.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SampleApp
{
    public class SampleAppContext : EntityContextBase<SampleAppContext>
    {
        public SampleAppContext(DbContextOptions<SampleAppContext> options) : base(options)
        { }

        public DbSet<Building> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.LowerCaseTablesAndFields();
        }
    }
}
