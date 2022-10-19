using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Data.Base;
using Project.Data;

namespace Project.Models
{
    public class Item:IEntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public ItemCategory ItemCategory { get; set; }
        [Required]
        public string PictureURL { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        [Required]
        public double Price { get; set; }
        public ItemSize itemsize { get; set; }

        // 1 to 1 relationship with store and brand
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}