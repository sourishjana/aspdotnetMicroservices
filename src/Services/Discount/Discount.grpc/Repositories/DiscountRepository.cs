using Discount.grpc.Entities;
using Discount.grpc.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly ProductDBContext _context;

    public DiscountRepository(ProductDBContext context)
    {
        _context = context;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.ProductName.Equals(productName));

        if (coupon == null)
            return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        _context.Coupons.Add(coupon);
        var affected = await _context.SaveChangesAsync();

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        if (coupon == null || coupon.Id == 0)
            return false;

        var couponData = await _context.Coupons.FindAsync(coupon.Id);
        if (couponData == null)
            return false;
        couponData.ProductName = coupon.ProductName;
        couponData.Description = coupon.Description;
        couponData.Amount = coupon.Amount;
        var affected = await _context.SaveChangesAsync();

        if (affected == 0)
            return false;
        return true;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        if (productName == null)
            return false;
        var couponData = await _context.Coupons.FirstOrDefaultAsync(c => c.ProductName.Equals(productName));
        if (couponData == null)
            return false;
        _context.Coupons.Remove(couponData);
        var affected = await _context.SaveChangesAsync();

        if (affected == 0)
            return false;

        return true;
    }
}
