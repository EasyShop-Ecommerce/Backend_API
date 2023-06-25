using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class CustmerRegisterDTO
    {

        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Invalid Email")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }

        public string Government { get; set; }
        [Required]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Invalid Egyptian Number")]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
