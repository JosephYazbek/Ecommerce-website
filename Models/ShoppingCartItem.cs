using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; } 
        public Item Item { get; set; }
        public int Ammount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}