using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        private ApplicationDbContext _context;
        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(string view = "Default")
        {
         
            return View(view,new GetCart(HttpContext.Session,_context).Do());
        }
    }
}
