﻿// <auto-generated />
using System;
using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Garage_2_0.Migrations
{
    [DbContext(typeof(Garage_2_0Context))]
    partial class Garage_2_0ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Garage_2_0.Models.Prices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Price");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Garage_2_0.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand");

                    b.Property<string>("Color");

                    b.Property<string>("FreeText");

                    b.Property<string>("Model");

                    b.Property<int>("NoWheels");

                    b.Property<DateTime>("ParkedIn");

                    b.Property<DateTime>("ParkedOut");

                    b.Property<string>("RegNr");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Volvo",
                            Color = "Grön",
                            FreeText = "Hejsan",
                            Model = "Amazon",
                            NoWheels = 4,
                            ParkedIn = new DateTime(2019, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            ParkedOut = new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999),
                            RegNr = "ABC123",
                            Type = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
