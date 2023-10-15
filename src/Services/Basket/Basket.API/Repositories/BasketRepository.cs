using Basket.API.Entities;
using Basket.API.Repositories.Interface;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _redisCache;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _redisCache = redis.GetDatabase();
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.StringGetAsync(userName);

        if (String.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _redisCache.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));

        return await GetBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.KeyDeleteAsync(userName);
    }
}
