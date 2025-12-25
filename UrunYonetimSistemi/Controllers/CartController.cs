using Microsoft.AspNetCore.Mvc;
using UrunYonetimSistemi.Extensions;
using UrunYonetimSistemi.Models;

namespace UrunYonetimSistemi.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private const string CartSessionKey = "Cart";

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            // Stokta hiç yoksa direkt hata ver
            if (product.Stock <= 0)
            {
                TempData["ErrorMessage"] = "Ürün stokta bulunmamaktadır!";
                return RedirectToAction("Index", "Home");
            }

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == id);

            if (existingItem != null)
            {
                // Sepette zaten varsa, artırınca stoğu geçiyor mu kontrol et
                if (existingItem.Quantity + 1 > product.Stock)
                {
                    TempData["ErrorMessage"] = $"Stok yetersiz! En fazla {product.Stock} adet ekleyebilirsiniz.";
                }
                else
                {
                     existingItem.Quantity++;
                     TempData["SuccessMessage"] = "Ürün adedi artırıldı!";
                }
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = 1
                });
                TempData["SuccessMessage"] = "Ürün sepete eklendi!";
            }

            SaveCart(cart);

            var referer = Request.Headers["Referer"].ToString();
            if(!string.IsNullOrEmpty(referer))
            {
                 return Redirect(referer);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DecreaseQuantity(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == id);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    TempData["SuccessMessage"] = "Ürün adedi azaltıldı!";
                }
                else
                {
                    cart.Remove(item);
                    TempData["SuccessMessage"] = "Ürün sepetten çıkarıldı!";
                }
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == id);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction(nameof(Index));
        }

        private List<CartItem> GetCart()
        {
            return HttpContext.Session.GetObject<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObject(CartSessionKey, cart);
        }
    }
}
