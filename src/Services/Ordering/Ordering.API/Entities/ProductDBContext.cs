using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ordering.API.Entities;

public class ProductDBContext : DbContext
{
    public ProductDBContext(DbContextOptions<ProductDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            //entity.HasNoKey();

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.AddressLine)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CardName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CardNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CVV)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CVV");

            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Expiration)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ZipCode)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
    }
}
