using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriMarket.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string FarmerName { get; set; }
        public string FarmerEmail { get; set; }
        public string FarmerNumber { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string ProductName { get; set; }

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public string? ProductImg { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, ErrorMessage = "Product description cannot exceed 500 characters.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0.")]
        public decimal ProductPrice { get; set; }
        // Product status (Pending, Accepted, Rejected)
        public FarmerProductStatus Status { get; set; } = FarmerProductStatus.Pending;
    }
    public enum FarmerProductStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
