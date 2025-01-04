using System.ComponentModel.DataAnnotations.Schema;

namespace AgriMarket.Models
{
    public class BestSeller
    {
        public int Id { get; set; }
        public String ProductURL {  get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public String ProductType { get; set; }
        public String? Productimg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
