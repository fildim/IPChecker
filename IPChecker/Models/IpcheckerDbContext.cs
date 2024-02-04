using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IPChecker.Models;

public partial class IpcheckerDbContext : DbContext
{
    public IpcheckerDbContext()
    {
    }

    public IpcheckerDbContext(DbContextOptions<IpcheckerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<IpAddress> IpAddresses { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("COUNTRIES");

            entity.HasIndex(e => e.Name, "IX_COUNTRIES_NAME").IsUnique();

            entity.HasIndex(e => e.ThreeLetterCode, "IX_COUNTRIES_THREELETTERCODE").IsUnique();

            entity.HasIndex(e => e.TwoLetterCode, "IX_COUNTRIES_TWOLETTERCODE").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityColumn<int>();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("CREATED_AT");
            
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            
            entity.Property(e => e.ThreeLetterCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("THREE_LETTER_CODE");
            
            entity.Property(e => e.TwoLetterCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("TWO_LETTER_CODE");
        });

        modelBuilder.Entity<IpAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IPADDRESSES");

            entity.ToTable("IP_ADDRESSES");

            entity.HasIndex(e => e.Ip, "IX_IPADDRESSES_IP").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityColumn<int>();

            entity.Property(e => e.CountryId)
                .HasColumnName("COUNTRY_ID");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("CREATED_AT");

            entity.Property(e => e.Ip)
                .HasMaxLength(15)
                .IsFixedLength()
                .IsUnicode(false)
                .HasColumnName("IP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("UPDATED_AT");

            entity.HasOne(d => d.Country).WithMany(p => p.IpAddresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IPADDRESSES_COUNTRIES")
                .IsRequired();
            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
