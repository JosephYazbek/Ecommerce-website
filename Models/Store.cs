using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Store
    {
        [Key]

        public int StoreId { get; set; }
        [Display(Name = "Store Logo")]
        [Required(ErrorMessage = "Store Logo is Required")]
        public string? StoreLogo { get; set; }
        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store Name is Required")]
        public string? StoreName { get; set; }
        [Display(Name = "Store Description")]
        [Required(ErrorMessage = "Store Description is Required")]
        public string? StoreDescription { get; set; }
        [Display(Name = "Store Location")]
        [Required(ErrorMessage = "Store Location is Required")]

        public string? Location { get; set; }

        //Relationships

        public ICollection<Item>? Items { get; set; }

    }
}