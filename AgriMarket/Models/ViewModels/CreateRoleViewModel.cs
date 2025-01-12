using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public String RoleName { get; set; }
    }
}
