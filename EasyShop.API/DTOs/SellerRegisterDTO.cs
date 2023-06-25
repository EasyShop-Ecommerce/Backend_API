using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class SellerRegisterDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string MiddleName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string LastName { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        [RegularExpression("^([1-9]{1})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})[0-9]{3}([0-9]{1})[0-9]{1}$")]
        public string SSN { get; set; }

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
        [Compare("Password", ErrorMessage = "Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
