using System.ComponentModel.DataAnnotations;

namespace Swiggy.Models
{
    public class CustomerModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public long PhoneNumber { get; set; }

    }
}
