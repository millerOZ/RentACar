﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentACar.Data;

#nullable disable

namespace RentACar.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220421154818_updateRentalType")]
    partial class updateRentalType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RentACar.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RentACar.Data.Entities.ImageVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("ImageVehicles");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int?>("ReserveId")
                        .HasColumnType("int");

                    b.Property<float>("TotalValue")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ReserveId");

                    b.HasIndex("Name", "ReserveId")
                        .IsUnique()
                        .HasFilter("[ReserveId] IS NOT NULL");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("RentACar.Data.Entities.RentalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("RentalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RentalId");

                    b.HasIndex("Name", "RentalId")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [RentalId] IS NOT NULL");

                    b.ToTable("RentalTypes");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Reserve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateFinishReserve")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReserve")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStartReserve")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlaceFinishReserve")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("StartReserve")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Reserves");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Plaque")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Serie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Plaque")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("RentACar.Data.Entities.VehicleCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VehicleId", "CategoryId")
                        .IsUnique()
                        .HasFilter("[VehicleId] IS NOT NULL AND [CategoryId] IS NOT NULL");

                    b.ToTable("VehicleCategories");
                });

            modelBuilder.Entity("RentACar.Data.Entities.ImageVehicle", b =>
                {
                    b.HasOne("RentACar.Data.Entities.Vehicle", "Vehicle")
                        .WithMany("ImageVehicles")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Rental", b =>
                {
                    b.HasOne("RentACar.Data.Entities.Reserve", "Reserve")
                        .WithMany("Rentals")
                        .HasForeignKey("ReserveId");

                    b.Navigation("Reserve");
                });

            modelBuilder.Entity("RentACar.Data.Entities.RentalType", b =>
                {
                    b.HasOne("RentACar.Data.Entities.Rental", "Rental")
                        .WithMany("RentalTypes")
                        .HasForeignKey("RentalId");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("RentACar.Data.Entities.VehicleCategory", b =>
                {
                    b.HasOne("RentACar.Data.Entities.Category", "Category")
                        .WithMany("vehicleCategories")
                        .HasForeignKey("CategoryId");

                    b.HasOne("RentACar.Data.Entities.Vehicle", "Vehicle")
                        .WithMany("VehicleCategories")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Category");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Category", b =>
                {
                    b.Navigation("vehicleCategories");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Rental", b =>
                {
                    b.Navigation("RentalTypes");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Reserve", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("RentACar.Data.Entities.Vehicle", b =>
                {
                    b.Navigation("ImageVehicles");

                    b.Navigation("VehicleCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
