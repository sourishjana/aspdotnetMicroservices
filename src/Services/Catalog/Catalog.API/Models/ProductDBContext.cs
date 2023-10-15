using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catalog.API.Models;

public class ProductDBContext : DbContext
{

    public ProductDBContext(DbContextOptions<ProductDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.ImageFile).IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.Summary)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
    }
}
