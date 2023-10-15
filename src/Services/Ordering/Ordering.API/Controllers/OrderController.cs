using Microsoft.AspNetCore.Mvc;
using Ordering.API.Entities;
using Ordering.API.Repositories.Interface;
using System.Net;

namespace Ordering.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _mediator;

    public OrderController(IOrderRepository mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userName}", Name = "GetOrder")]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserName(string userName)
    {
        var orders = await _mediator.GetOrder(userName);
        return Ok(orders);
    }

    // testing purpose
    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] Order command)
    {
        var result = await _mediator.CreateOrder(command);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOrder([FromBody] Order command)
    {
        await _mediator.UpdateOrder(command);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteOrder(string id)
    {
        await _mediator.DeleteOrder(id);
        return NoContent();
    }
}