using System.ComponentModel.DataAnnotations;

namespace Swiggy.Models
{
    public class AddUserModel
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Mobile { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
