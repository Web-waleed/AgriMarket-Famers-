using System.ComponentModel.DataAnnotations.Schema;

namespace AgriMarket.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProducName { get; set; }
        public String? Productimg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
         public String ProductDescription { get; set; }

        public decimal? ProductPrice { get; set; }  


    }
}
