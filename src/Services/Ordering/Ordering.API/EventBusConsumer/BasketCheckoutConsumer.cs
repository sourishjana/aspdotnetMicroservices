using AutoMapper;
using MassTransit;
using Ordering.API.Entities;
using Ordering.API.Repositories.Interface;
using RabbitMqEventBus.Messages.Events;

namespace Ordering.API.EventBusConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IOrderRepository _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketCheckoutConsumer> _logger;

    public BasketCheckoutConsumer(IOrderRepository mediator, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<Order>(context.Message);
        var result = await _mediator.CreateOrder(command);

        _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
    }
}
