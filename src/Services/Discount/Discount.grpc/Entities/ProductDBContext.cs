using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Entities;

public class ProductDBContext : DbContext
{

    public ProductDBContext(DbContextOptions<ProductDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coupon> Coupons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("Coupon");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
    }
}
