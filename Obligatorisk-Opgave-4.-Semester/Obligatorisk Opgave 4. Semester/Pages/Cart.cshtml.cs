using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Obligatorisk_Opgave_4._Semester.Models;
using Obligatorisk_Opgave_4._Semester.Services;

namespace Obligatorisk_Opgave_4._Semester.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;

        public Cart Cart { get; set; } = new Cart();

        public CartModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        public void OnGet()
        {
            Cart = _cartService.GetCart();
        }

        public IActionResult OnPostClear()
        {
            _cartService.ClearCart();
            return RedirectToPage();
        }
    }
}
