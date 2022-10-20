using System.ComponentModel.DataAnnotations;

namespace Swiggy.Models
{
    public class ProductsModel
    {
        [Required]
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int ProductPrice { get; set; }

    }
}
