using System.ComponentModel.DataAnnotations.Schema;

namespace AgriMarket.Models
{
    public class Slider
    {
        public int SliderID { get; set; }
        public int SliderNum { get; set; }
        public String SliderHeader { get; set; }
        public String SliderDescription  { get; set; }
        public String Button {  get; set; }
        public String? Sliderimg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
