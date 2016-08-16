using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccess.SampleApp;

namespace DataAccess.SampleApp.Migrations
{
    [DbContext(typeof(SampleAppContext))]
    partial class SampleAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Appartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingId");

                    b.Property<int>("Floor");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Appartment");
                });

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppartmentId");

                    b.Property<int>("Length");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfDoors");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("AppartmentId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Appartment", b =>
                {
                    b.HasOne("DataAccess.SampleApp.Entities.Building")
                        .WithMany("Appartments")
                        .HasForeignKey("BuildingId");
                });

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Room", b =>
                {
                    b.HasOne("DataAccess.SampleApp.Entities.Appartment")
                        .WithMany("Rooms")
                        .HasForeignKey("AppartmentId");
                });
        }
    }
}
