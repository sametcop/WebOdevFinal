using System.ComponentModel.DataAnnotations;

namespace UrunYonetimSistemi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
