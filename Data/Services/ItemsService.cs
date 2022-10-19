using Microsoft.EntityFrameworkCore;
using Project.Data.Base;
using Project.Data.ViewModels;
using Project.Models;

namespace Project.Data.Services
{
    public class ItemsService : EntityBaseRepository<Item>, IItemsService
    {
        private readonly AppDbContext _context;
        public ItemsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewItemAsync(NewItemVM data)
        {
            var newItem = new Item()
            {
                ItemName = data.ItemName,
                ItemCategory = data.ItemCategory,
                PictureURL = data.PictureURL,
                ItemDescription = data.ItemDescription,
                Price = data.Price,
                itemsize = data.itemsize,
                StoreId = data.StoreId,
                BrandId = data.BrandId
            };
            await _context.Items.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            var itemDetails = await _context.Items
            .Include(c => c.Store)
            .Include(b => b.Brand)
            .FirstOrDefaultAsync(n => n.Id == id);
            return itemDetails;
        }

        public async Task<NewItemDropdownsVM> GetNewItemDropdownsValues()
        {
            var response = new NewItemDropdownsVM()
            {
                Stores = await _context.Stores.OrderBy(n => n.StoreName).ToListAsync(),
                Brands = await _context.Brands.OrderBy(n => n.BrandName).ToListAsync(),
            };
            return response;

        }

        public async Task UpdateItemAsync(NewItemVM data)
        {
            var dbItem = await _context.Items.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbItem != null)
            {
                dbItem.ItemName = data.ItemName;
                dbItem.ItemCategory = data.ItemCategory;
                dbItem.PictureURL = data.PictureURL;
                dbItem.ItemDescription = data.ItemDescription;
                dbItem.Price = data.Price;
                dbItem.itemsize = data.itemsize;
                dbItem.StoreId = data.StoreId;
                dbItem.BrandId = data.BrandId;

                await _context.SaveChangesAsync();
            }
            // Removing existing brands


        }
    }
}