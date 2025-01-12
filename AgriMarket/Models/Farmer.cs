using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models
{
    public class Farmer
    {
       
        public int FarmerId { get; set; }

        [Required(ErrorMessage = "Farmer name is required.")]
        public string FarmerName { get; set; }

        [Required(ErrorMessage = "Farmer email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string FarmerEmail { get; set; }

        [Required(ErrorMessage = "Farmer number is required.")]
        public string FarmerNumber { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        

    }
}
