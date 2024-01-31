﻿// <auto-generated />
using System;
using IPChecker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IPChecker.Migrations
{
    [DbContext(typeof(IpcheckerDbContext))]
    partial class IpcheckerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IPChecker.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NAME");

                    b.Property<string>("ThreeLetterCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .HasColumnName("THREE_LETTER_CODE")
                        .IsFixedLength();

                    b.Property<string>("TwoLetterCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nchar(2)")
                        .HasColumnName("TWO_LETTER_CODE")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_COUNTRIES_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "ThreeLetterCode" }, "IX_COUNTRIES_THREELETTERCODE")
                        .IsUnique();

                    b.HasIndex(new[] { "TwoLetterCode" }, "IX_COUNTRIES_TWOLETTERCODE")
                        .IsUnique();

                    b.ToTable("COUNTRIES", (string)null);
                });

            modelBuilder.Entity("IPChecker.Models.IpAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("COUNTRY_ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("IP");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id")
                        .HasName("PK_IPADDRESSES");

                    b.HasIndex("CountryId");

                    b.HasIndex(new[] { "Ip" }, "IX_IPADDRESSES_IP")
                        .IsUnique();

                    b.ToTable("IP_ADDRESSES", (string)null);
                });

            modelBuilder.Entity("IPChecker.Models.IpAddress", b =>
                {
                    b.HasOne("IPChecker.Models.Country", "Country")
                        .WithMany("IpAddresses")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK_IPADDRESSES_COUNTRIES");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("IPChecker.Models.Country", b =>
                {
                    b.Navigation("IpAddresses");
                });
#pragma warning restore 612, 618
        }
    }
}
