using System.ComponentModel.DataAnnotations;

namespace Swiggy.Models
{
    public class AddProductModel
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int ProductPrice { get; set; }
    }
}
