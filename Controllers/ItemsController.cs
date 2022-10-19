using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Data;
using Project.Data.Services;
using Project.Models;

namespace Project.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemsService _service;

        public ItemsController(IItemsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Store);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync(n => n.Store);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = data.Where(n => n.ItemName.Contains(searchString) || n.ItemDescription.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", data); // else return all items
        }




        //Get: Items / Details / Id
        public async Task<IActionResult> Details(int id)
        { // passing action to the view
            var itemDetails = await _service.GetItemByIdAsync(id);
            return View(itemDetails);
        }
        //Get: Items / Create / Id
        public async Task<IActionResult> Create()
        {
            // ViewData["Welcome"] = "Welcome to our Store";
            // ViewBag.itemDetails = "This is the store Details";
            var itemDropdownsData = await _service.GetNewItemDropdownsValues();
            ViewBag.Stores = new SelectList(itemDropdownsData.Stores, "StoreId", "StoreName");
            ViewBag.Brands = new SelectList(itemDropdownsData.Brands, "BrandId", "BrandName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewItemVM item)
        {
            if (!ModelState.IsValid)
            {
                // var itemDropdownsData = await _service.GetNewItemDropdownsValues();
                // ViewBag.Stores = new SelectList(itemDropdownsData.Stores, "StoreId", "StoreName");
                // ViewBag.Brands = new SelectList(itemDropdownsData.Brands, "BrandId", "BrandName");
                return View(item);
            }
            await _service.AddNewItemAsync(item);
            return RedirectToAction(nameof(Index));
        }

        //------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Edit(int id)
        {
            // ViewData["Welcome"] = "Welcome to our Store";
            // ViewBag.itemDetails = "This is the store Details";
            var itemDetails = await _service.GetItemByIdAsync(id);
            if (itemDetails == null) return View("NotFound");

            var response = new NewItemVM()
            {
                Id = itemDetails.Id,
                ItemName = itemDetails.ItemName,
                ItemCategory = itemDetails.ItemCategory,
                PictureURL = itemDetails.PictureURL,
                ItemDescription = itemDetails.ItemDescription,
                Price = itemDetails.Price,
                itemsize = itemDetails.itemsize,
                StoreId = itemDetails.StoreId,
                BrandId = itemDetails.BrandId
            };

            var itemDropdownsData = await _service.GetNewItemDropdownsValues();
            ViewBag.Stores = new SelectList(itemDropdownsData.Stores, "StoreId", "StoreName");
            ViewBag.Brands = new SelectList(itemDropdownsData.Brands, "BrandId", "BrandName");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewItemVM item)
        {
            if (id != item.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                // var itemDropdownsData = await _service.GetNewItemDropdownsValues();
                // ViewBag.Stores = new SelectList(itemDropdownsData.Stores, "StoreId", "StoreName");
                // ViewBag.Brands = new SelectList(itemDropdownsData.Brands, "BrandId", "BrandName");
                return View(item);
            }
            await _service.UpdateItemAsync(item);
            return RedirectToAction(nameof(Index));
        }
    }
}