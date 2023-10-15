using AutoMapper;
using Ordering.API.Entities;
using RabbitMqEventBus.Messages.Events;

namespace Ordering.API.Mapper;

public class OrderingProfile : Profile
{
	public OrderingProfile()
	{
		CreateMap<Order, BasketCheckoutEvent>().ForMember(dest => dest.Id, opt => opt.Ignore());
		CreateMap<BasketCheckoutEvent, Order>().ForMember(dest => dest.Id, opt => opt.Ignore());
	}
}
