using AutoMapper;
using Discount.grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.grpc.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}
