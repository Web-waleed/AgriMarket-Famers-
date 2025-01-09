using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public String? Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public String? Password { get; set; }
    }
}
