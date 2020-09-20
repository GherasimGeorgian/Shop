using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        private ApplicationDbContext _context;
        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public GetCart.Response Cart { get; set; }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _context).Do();
            return Page();
        }
    }
}
