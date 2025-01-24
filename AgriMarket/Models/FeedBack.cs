using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models
{
    public class FeedBack
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public  string Name { get; set; } 

        [Required(ErrorMessage = "Review is required.")]
        [StringLength(2000, ErrorMessage = "Review cannot exceed 500 characters.")]
        public  string Review { get; set; } 

        
        public string? SubmissionDate { get; set; }

        
        public string? Active { get; set; } = "true"; 
    }
}
