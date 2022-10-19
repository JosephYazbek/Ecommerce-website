using Project.Models;
namespace Project.Data.ViewModels{
    public class NewItemDropdownsVM{
        public NewItemDropdownsVM()
        {
            Stores = new List<Store>();
            Brands = new List<Brand>();
        }
        public List <Store> Stores { get; set; }
        public List <Brand> Brands { get ; set; }


    }
}