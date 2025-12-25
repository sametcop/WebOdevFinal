using UrunYonetimSistemi.Models;

namespace UrunYonetimSistemi.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<SliderItem> Sliders { get; set; } = new List<SliderItem>();
    }
}
