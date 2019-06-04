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

            modelBuilder.Entity("Garage_2_0.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

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

                    b.Property<int>("MemberId");

                    b.Property<string>("Model");

                    b.Property<int>("NoWheels");

                    b.Property<DateTime>("ParkedIn");

                    b.Property<string>("RegNr");

                    b.Property<int>("VehicleTypeClassId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("VehicleTypeClassId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Garage_2_0.Models.VehicleTypeClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Price");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("VehicleTypeClass");
                });

            modelBuilder.Entity("Garage_2_0.Models.Vehicle", b =>
                {
                    b.HasOne("Garage_2_0.Models.Member")
                        .WithMany("Vehicles")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Garage_2_0.Models.VehicleTypeClass")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
