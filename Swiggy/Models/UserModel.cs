using System.ComponentModel.DataAnnotations;

namespace Swiggy.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? UserName { get; set; }
        public string? Mobile { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
      
    }
}
