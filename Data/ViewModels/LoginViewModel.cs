using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
public class LoginViewModel
{
    [Required(ErrorMessage = "Please Enter a Username")]
    [StringLength(225)]
    public string Username {get; set;}

    [Required(ErrorMessage = "Please Enter a Username")]
    [StringLength(225)]
    public string Password { get; set; }    
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }

}
}