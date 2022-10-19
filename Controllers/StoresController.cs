using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class StoresController : Controller
    {
        public static List<Store> stores = new List<Store>();
        private readonly AppDbContext _context;

        public StoresController(AppDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            var data = _context.Stores.ToList();
            return View(data);
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("StoreLogo,StoreName,StoreDescription,Location")] Store store)
        {
            if (!ModelState.IsValid)
            { // if it is not valid we dont add to database

                return View(store);
            }
            else
            {
                Store s1 = new Store();
                s1.StoreName = store.StoreName;
                s1.StoreLogo = store.StoreLogo;
                s1.StoreId = store.StoreId;
                s1.StoreDescription = store.StoreDescription;
                s1.Location = store.Location;

                _context.Stores.Add(s1);
                _context.SaveChanges();
                var sto = _context.Brands.ToList();
                stores.Add(store);
                return RedirectToAction("Index", sto);
            }
        }




        //------------------------------------------------------------------------------------------------------

        public IActionResult Details(int Id)
        {
            Store s = _context.Stores.Find(Id);
            if (s == null) return View("NotFound");

            return View(s);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Stores.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Store s)
        {
            _context.Stores.Update(s);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {

            _context.Stores.Remove(_context.Stores.Find(id));
            _context.SaveChanges();
            var stor = _context.Stores.ToList();
            if (stor == null) return View("NotFound");

            return View("Index", stor);
        }
    }
}