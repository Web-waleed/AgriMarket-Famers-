using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriMarket.Models
{
    public class Slider
    {
        public int SliderID { get; set; }

        [Required(ErrorMessage = "Slider number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Slider number must be at least 1.")]
        public int SliderNum { get; set; }

        [Required(ErrorMessage = "Slider header is required.")]
        [StringLength(100, ErrorMessage = "Slider header cannot exceed 100 characters.")]
        public string SliderHeader { get; set; } 

        [Required(ErrorMessage = "Slider description is required.")]
        [StringLength(500, ErrorMessage = "Slider description cannot exceed 500 characters.")]
        public string SliderDescription { get; set; } 

        [Required(ErrorMessage = "Button text is required.")]
        [StringLength(50, ErrorMessage = "Button text cannot exceed 50 characters.")]
        public string Button { get; set; } 

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public string? Sliderimg { get; set; } 

        [NotMapped]
        public IFormFile? ImageFile { get; set; } 
    }
}