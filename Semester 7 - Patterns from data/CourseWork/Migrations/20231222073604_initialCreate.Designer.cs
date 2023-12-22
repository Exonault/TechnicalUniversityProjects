﻿// <auto-generated />
using System;
using CourseWork;
using CourseWork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseWork.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231222073604_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseWork.DataEntry", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RequestId"));

                    b.Property<int>("AddressRegion")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("ApplicationDate")
                        .HasColumnType("date");

                    b.Property<decimal>("ApprovedAmount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsNewClient")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPaidOff")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRefinance")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRefinanced")
                        .HasColumnType("boolean");

                    b.Property<decimal>("LendedAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("Period")
                        .HasColumnType("integer");

                    b.Property<string>("Product")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("RepaidAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RequestId");

                    b.ToTable("DataSet", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}