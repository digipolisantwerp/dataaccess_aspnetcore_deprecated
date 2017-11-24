using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccess.SampleApp;

namespace DataAccess.SampleApp.Migrations
{
    [DbContext(typeof(SampleAppContext))]
    [Migration("20171124112404_lowercasedtablesandfieldswithcolumnattribute")]
    partial class lowercasedtablesandfieldswithcolumnattribute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Appartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("BuildingId")
                        .HasColumnName("buildingid");

                    b.Property<int>("Floor")
                        .HasColumnName("floor");

                    b.Property<int>("Number")
                        .HasColumnName("number");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("appartment");
                });

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("building");
                });

            modelBuilder.Entity("DataAccess.SampleApp.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AppartmentId")
                        .HasColumnName("appartmentid");

                    b.Property<int>("Length")
                        .HasColumnName("length");

                    b.Property<string>("Name")
                        .HasColumnName("mynewroomname");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnName("numberofdoors");

                    b.Property<int>("Width")
                        .HasColumnName("width");

                    b.HasKey("Id");

                    b.HasIndex("AppartmentId");

                    b.ToTable("room");
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
