﻿using AutoMapper;
using Basket.API.Entities;
using RabbitMqEventBus.Messages.Events;

namespace Basket.API.Mapper;

public class BasketProfile : Profile
{
	public BasketProfile()
	{
		CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
	}
}
