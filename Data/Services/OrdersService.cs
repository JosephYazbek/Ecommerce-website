using Project.Models;
using Microsoft.EntityFrameworkCore;
namespace Project.Data.Services;

public class OrdersService: IOrdersService
{
    private readonly AppDbContext ctx;

    public OrdersService(AppDbContext cont)
    {
        ctx = cont;
    }

    public async Task StoreOrderAsync (List<ShoppingCartItem> items , string userId, string userEmaiAdress)
    {
        var order = new Order()
        {
            UserId = userId,
            Email = userEmaiAdress             
        };
        await ctx.Orders.AddAsync(order);
        await ctx.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderitem = new OrderItem()
            {
                Ammount = item.Ammount,
                ItemId = item.Id,
                OrderId = order.Id,
                Price = item.Item.Price
            };
            await ctx.OrderItems.AddAsync(orderitem);
        }
        await ctx.SaveChangesAsync();
    }
    public async Task <List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        var orders = await ctx.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Item).Where(n => n.UserId == userId).ToListAsync();
        return orders;
    }

}