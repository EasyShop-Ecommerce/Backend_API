using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
