using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _context;
      
        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response> Do(Request req)
        {
             var Product = _context.Products.FirstOrDefault(x => x.Id == req.Id);
            Product.Name = req.Name;
            Product.Description = req.Description;
            Product.Value = decimal.Parse(req.Value);

            await _context.SaveChangesAsync();
            return new Response
            {
                Id = Product.Id,
                Name = Product.Name,
                Description = Product.Description,
                Value = Product.Value
            };
        }
        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
   
}
