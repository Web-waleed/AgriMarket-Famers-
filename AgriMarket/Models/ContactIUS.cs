using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models
{
    public class ContactIUS
    {
        public int Id { get; set; }
        [Required]
        public String FullName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public String PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Message { get; set; }
    }
}
