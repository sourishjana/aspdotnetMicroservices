using Microsoft.EntityFrameworkCore;
using Ordering.API.Entities;
using Ordering.API.Repositories.Interface;

namespace Ordering.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ProductDBContext _context;

    public OrderRepository(ProductDBContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        var affected = await _context.SaveChangesAsync();

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> DeleteOrder(string userName)
    {
        if (userName == null)
            return false;
        var orderData = await _context.Orders.FirstOrDefaultAsync(c => c.UserName.Equals(userName));
        if (orderData == null)
            return false;
        _context.Orders.Remove(orderData);
        var affected = await _context.SaveChangesAsync();

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<Order> GetOrder(string userName)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.UserName.Equals(userName));

        if (order == null)
            return new Order();
        return order;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        if (order == null)
            return false;

        var orderData = await _context.Orders.FirstOrDefaultAsync(o => o.UserName.Equals(order.UserName));
        if (orderData == null)
            return false;
        orderData.FirstName = order.FirstName;
        orderData.LastName = order.LastName;
        var affected = await _context.SaveChangesAsync();

        if (affected == 0)
            return false;
        return true; ;
    }
}
