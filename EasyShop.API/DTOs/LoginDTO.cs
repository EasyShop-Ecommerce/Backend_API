using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class LoginDTO
    {
        public int? CustomerId { get; set; }
        public int? sellerId { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime Expiration {get; set; }
        public string name { get; set; }
    }
}
