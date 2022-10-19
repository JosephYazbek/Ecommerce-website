using Microsoft.AspNetCore.Mvc;
using Project.Data.Services;
using Project.Data.ViewModels;
using Project.Data.Cart;
namespace Project.Controllers;


public class OrdersController: Controller
{
    private readonly IItemsService itser;
    private readonly ShoppingCart sc;
    private readonly IOrdersService orser;
    public OrdersController( IItemsService itemsService, ShoppingCart scs, IOrdersService or )
    {
        itser = itemsService;
        sc = scs;
        orser = or;
    }

    public IActionResult ShoppingCart()
    {
        var items = sc.GetShoppingCartItems();
        sc.ShoppingCartItems = items;
        var response = new ShoppingCartViewModel()
        {
            ShoppingCart = sc,
            ShoppingCartTotal = sc.GetShoppingCartTotal()
        };
        return View(response);
    }
    public async Task<IActionResult> AddToCart (int Id)
    {
        var item = await itser.GetItemByIdAsync(Id);    

        if ( item != null)
        {
            sc.AddToCart(item);
        }
        return RedirectToAction(nameof(ShoppingCart)); 
    }

    public async Task<IActionResult> RemoveFromCart (int Id)
    {
        var item = await itser.GetItemByIdAsync(Id);    

        if ( item != null)
        {
            sc.RemoveFromCart(item);
        }
        return RedirectToAction(nameof(Index)); 
    }
    
    public async Task<IActionResult> CompleteOrder ()
    {
        var items = sc.GetShoppingCartItems();
        String userId = "";
        string userEmaiAdress = "";
        await orser.StoreOrderAsync(items, userId, userEmaiAdress);
        await sc.ClearShoppingCartAsync();
        return View();  

    }

}