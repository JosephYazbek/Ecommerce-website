using Microsoft.AspNetCore.Mvc;
using Project.Data.Cart;
namespace Project.Data.ViewComponents
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart sc ; 
        public ShoppingCartSummary( ShoppingCart scsss)
        {
            sc = scsss;
        }

        public IViewComponentResult Invoke()
        {
            var items = sc.GetShoppingCartItems();

            return View(items.Count);
        }

    }
    
}