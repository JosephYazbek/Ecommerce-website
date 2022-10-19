using Project.Data.Base;
using Project.Data.ViewModels;
using Project.Models;

namespace Project.Data.Services
{
    public interface IItemsService : IEntityBaseRepository<Item>
    {
        Task<Item> GetItemByIdAsync(int id);
        Task<NewItemDropdownsVM> GetNewItemDropdownsValues();
        Task AddNewItemAsync(NewItemVM data);
        Task UpdateItemAsync(NewItemVM data);

    }
}