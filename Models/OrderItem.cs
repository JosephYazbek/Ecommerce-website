using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int Ammount { get; set; }
        public double Price { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item {get; set;}

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}