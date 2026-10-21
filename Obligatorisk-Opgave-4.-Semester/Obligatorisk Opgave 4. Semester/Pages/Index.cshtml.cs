using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Obligatorisk_Opgave_4._Semester.Models;
using Obligatorisk_Opgave_4._Semester.Services;
using System.Collections.Generic;
using System.Linq;

namespace Obligatorisk_Opgave_4._Semester.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Obligatorisk_Opgave_4._Semester.Abstractions.IProductService _productService;
        private readonly ICartService _cartService;

        public IList<Product> Products { get; set; } = new List<Product>();

        public IndexModel(ILogger<IndexModel> logger, Obligatorisk_Opgave_4._Semester.Abstractions.IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public void OnGet()
        {
            Products = _productService.GetAll().ToList();
        }

        public IActionResult OnPostAddToCart(int id, int quantity = 1)
        {
            if (quantity < 1) quantity = 1;
            var success = _cartService.AddToCart(id, quantity);
            if (!success)
            {
                TempData["Error"] = "Kunne ikke tilføje produkt til kurven (muligvis ingen nok på lager).";
            }
            return RedirectToPage();
        }
    }
}
