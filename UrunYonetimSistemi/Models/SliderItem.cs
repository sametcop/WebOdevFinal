using System.ComponentModel.DataAnnotations;

namespace UrunYonetimSistemi.Models
{
    public class SliderItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Görsel")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Sıra")]
        public int Order { get; set; }
    }
}
