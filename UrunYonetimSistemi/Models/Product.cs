using System.ComponentModel.DataAnnotations;

namespace UrunYonetimSistemi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Display(Name = "Fiyat")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Eski Fiyat (İndirimsiz)")]
        [DataType(DataType.Currency)]
        public decimal? OldPrice { get; set; }

        [Required(ErrorMessage = "Stok adedi zorunludur.")]
        [Display(Name = "Stok Adedi")]
        public int Stock { get; set; } = 0;

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Ürün Görseli")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }

        [Display(Name = "Kategori")]
        public Category? Category { get; set; }
    }
}
