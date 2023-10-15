using Ordering.API.Entities;

namespace Ordering.API.Repositories.Interface;

public interface IOrderRepository
{
    Task<Order> GetOrder(string userName);

    Task<bool> CreateOrder(Order order);
    Task<bool> UpdateOrder(Order order);
    Task<bool> DeleteOrder(string userName);
}
