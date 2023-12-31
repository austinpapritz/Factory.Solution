﻿// <auto-generated />
using System;
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Factory.Migrations
{
    [DbContext(typeof(FactoryContext))]
    [Migration("20230804224655_MoreLicenses")]
    partial class MoreLicenses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Property<int>("EngineerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("EngineerId");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Factory.Models.EngineerLicense", b =>
                {
                    b.Property<int>("EngineerId")
                        .HasColumnType("int");

                    b.Property<int>("LicenseId")
                        .HasColumnType("int");

                    b.HasKey("EngineerId", "LicenseId");

                    b.HasIndex("LicenseId");

                    b.ToTable("EngineerLicenses");
                });

            modelBuilder.Entity("Factory.Models.License", b =>
                {
                    b.Property<int>("LicenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("LicenseId");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EngineerId")
                        .HasColumnType("int");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MachineId");

                    b.HasIndex("EngineerId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Factory.Models.MachineLicense", b =>
                {
                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<int>("LicenseId")
                        .HasColumnType("int");

                    b.HasKey("MachineId", "LicenseId");

                    b.HasIndex("LicenseId");

                    b.ToTable("MachineLicenses");
                });

            modelBuilder.Entity("Factory.Models.EngineerLicense", b =>
                {
                    b.HasOne("Factory.Models.Engineer", "Engineer")
                        .WithMany("EngineerLicenses")
                        .HasForeignKey("EngineerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.Models.License", "License")
                        .WithMany()
                        .HasForeignKey("LicenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineer");

                    b.Navigation("License");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.HasOne("Factory.Models.Engineer", null)
                        .WithMany("Machines")
                        .HasForeignKey("EngineerId");
                });

            modelBuilder.Entity("Factory.Models.MachineLicense", b =>
                {
                    b.HasOne("Factory.Models.License", "License")
                        .WithMany()
                        .HasForeignKey("LicenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.Models.Machine", "Machine")
                        .WithMany("MachineLicenses")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("License");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Navigation("EngineerLicenses");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Navigation("MachineLicenses");
                });
#pragma warning restore 612, 618
        }
    }
}
