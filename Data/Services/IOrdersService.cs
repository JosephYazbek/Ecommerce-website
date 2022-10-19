using Project.Models;
namespace Project.Data.Services;

public interface IOrdersService
{
    Task StoreOrderAsync (List<ShoppingCartItem> items , string userId, string userEmaiAdress);
    Task <List<Order>> GetOrdersByUserIdAsync(string userId);
}