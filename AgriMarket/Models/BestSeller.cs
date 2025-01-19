using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriMarket.Models
{
    public class BestSeller
    {
        public int Id { get; set; }

        

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, ErrorMessage = "Product description cannot exceed 500 characters.")]
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Product type is required.")]
        [StringLength(50, ErrorMessage = "Product type cannot exceed 50 characters.")]
        public string ProductType { get; set; }

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public string? ProductImg { get; set; } 
        [NotMapped]
        public IFormFile? ImageFile { get; set; } 
    }
}