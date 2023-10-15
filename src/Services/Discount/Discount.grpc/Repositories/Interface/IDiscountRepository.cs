﻿using Discount.grpc.Entities;

namespace Discount.grpc.Repositories.Interface;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);

    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}
