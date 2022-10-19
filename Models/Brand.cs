using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Display(Name = "LogoURL")]
        [Required(ErrorMessage = "Logo URL is Required")] // require to have a Logo
        public string? LogoURL { get; set; }

//------------------------------------------------------------------------------------------------

        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "Brand Name is Required")] // require to have a Logo
        // [StringLength(20, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 20 Characters")] // putting conditions
        public string? BrandName { get; set; }

//------------------------------------------------------------------------------------------------

        [Display(Name = "Brand Detail")]
        [Required(ErrorMessage = "Brand Detail is Required")] // require to have a Logo
        public string? BrandDetail { get; set; }

//------------------------------------------------------------------------------------------------

        public ICollection<Item>? Items { get; set; }

    }
}