using System.ComponentModel.DataAnnotations;

namespace Swiggy.Models
{
    public class OrdersModel
    {
        [Required]
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        public string? OrderName { get; set; }
        [Required]
        public int OrderPrice { get; set; }
    }
}
