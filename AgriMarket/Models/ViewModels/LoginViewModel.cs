using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Incorrect email address.")]
        public String? Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public String? Password { get; set; }
    }
}
