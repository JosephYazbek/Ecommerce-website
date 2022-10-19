using Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace Project.Data.Cart;



public class ShoppingCart 
{
    public AppDbContext ctx {get; set;}

    public string ShoppingCartId { get; set; }

    public List<ShoppingCartItem> ShoppingCartItems {get; set;}

    public ShoppingCart(AppDbContext context)
    {
        ctx = context;
    }

    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetRequiredService<AppDbContext>();
        string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString() ;
        session.SetString("cartId", CartId);

        return new ShoppingCart(context){ ShoppingCartId = CartId};
            
    }
    public void AddToCart(Item item)
    {
        var ShoppingCartItem  = ctx.ShoppingCartItems.FirstOrDefault(n => n.Item.Id == item.Id && n.ShoppingCartId == ShoppingCartId);

        if (ShoppingCartItem == null )
        {
            ShoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                Item = item,
                Ammount = 1
            };

            ctx.ShoppingCartItems.Add(ShoppingCartItem);
        }
        else
        {
            ShoppingCartItem.Ammount ++;
        }
        ctx.SaveChanges();
    }

    public void RemoveFromCart(Item item)
    {
        var ShoppingCartItem  = ctx.ShoppingCartItems.FirstOrDefault(n => n.Item.Id == item.Id && n.ShoppingCartId == ShoppingCartId);

        if (ShoppingCartItem != null )
        {
            if (ShoppingCartItem.Ammount >1)
            {
                ShoppingCartItem.Ammount --;
            }
            else
            {
                ctx.ShoppingCartItems.Remove(ShoppingCartItem);
            }      
        }
        ctx.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ?? (ShoppingCartItems = ctx.ShoppingCartItems.Where(n=> n.ShoppingCartId == ShoppingCartId).Include(n => n.Item).ToList());
    }

    public double GetShoppingCartTotal() => ctx.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Item.Price *n.Ammount).Sum();
    
    public async Task ClearShoppingCartAsync()
    {
        var items = await ctx.ShoppingCartItems.Where(n=> n.ShoppingCartId == ShoppingCartId).ToListAsync();
        ctx.ShoppingCartItems.RemoveRange(items);
        await ctx.SaveChangesAsync();
    }

}