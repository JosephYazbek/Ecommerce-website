using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Data.Base;
using Project.Data;

namespace Project.Models
{
    public class NewItemVM
    {
        public int Id { get; set; }
        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item Name is Required")]
        public string ItemName { get; set; }

        [Display(Name = "Select Item Category")]
        [Required(ErrorMessage = "Item Category is Required")]
        public ItemCategory ItemCategory { get; set; }

        [Display(Name = "Item URL")]
        [Required(ErrorMessage = "Item URL is Required")]
        public string PictureURL { get; set; }

        [Display(Name = "Item Description")]
        [Required(ErrorMessage = "Item Description is Required")]
        public string ItemDescription { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Item Price is Required")]
        public double Price { get; set; }

        [Display(Name = "Item Size")]
        [Required(ErrorMessage = "Item Size is Required")]
        public ItemSize itemsize { get; set; }
 
        // 1 to 1 relationship with store and brand
        [Display(Name = "Select Store(s)")]
        [Required(ErrorMessage = "Store is Required")]
        public int StoreId { get; set; }

        [Display(Name = "Select Brand")]
        [Required(ErrorMessage = "Brand is Required")]
        public int BrandId { get; set; }
    }
}