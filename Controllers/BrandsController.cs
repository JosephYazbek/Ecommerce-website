using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class BrandsController : Controller
    {
        public static List<Brand> brands = new List<Brand>();
        private readonly AppDbContext _context;

        public BrandsController(AppDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            var data = _context.Brands.ToList();
            return View(data);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //sending a post request from the create function create.cshtml (submit)
        public IActionResult Create([Bind("LogoURL,BrandName,BrandDetail")] Brand brand)
        { // we have 3 properties in the Index of the view Brand but in the Brand model we have the ID therefore 4 properties
            if (!ModelState.IsValid)
            { // if it is not valid we dont add to database
                return View(brand);
            }
            else
            {
                Brand b1 = new Brand();
                b1.BrandName = brand.BrandName;
                b1.LogoURL = brand.LogoURL;
                b1.BrandId = brand.BrandId;
                b1.BrandDetail = brand.BrandDetail;

                _context.Brands.Add(b1);
                _context.SaveChanges();
                var bran = _context.Brands.ToList();
                brands.Add(brand);
                return View("Index", bran);
            }
        }




        //------------------------------------------------------------------------------------------------------





        // creating another get request
        //Get : Brands / Details / ID
        public IActionResult Details(int Id)
        {
            // Brand b= brands.Find(x => x.BrandId == Id);
            Brand b = _context.Brands.Find(Id);
            if (b == null) return View("NotFound"); // in the shared folder

            return View(b);
        }





        //------------------------------------------------------------------------------------------------------
        //Get: Brands / Edit /Id
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Brands.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Brand b)
        {
            _context.Brands.Update(b);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //------------------------------------------------------------------------------------------------------
        //Get: Brands / Delete /ID
        public IActionResult Delete(int id)
        {
            _context.Brands.Remove(_context.Brands.Find(id));
            _context.SaveChanges();
            var bran = _context.Brands.ToList();
            if (bran == null) return View("NotFound");

            return View("Index", bran);
        }
        
    }
}