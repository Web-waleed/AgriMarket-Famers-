using System.ComponentModel.DataAnnotations;

namespace AgriMarket.Models
{
    public class FeedBack
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } // Changed to lowercase 'string' for consistency

        [Required(ErrorMessage = "Review is required.")]
        [StringLength(500, ErrorMessage = "Review cannot exceed 500 characters.")]
        public string Review { get; set; } // Changed to lowercase 'string' for consistency

        [DataType(DataType.DateTime)]
        public DateTime SubmissionDate { get; set; }
    }
}
