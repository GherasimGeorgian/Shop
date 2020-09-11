using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private ApplicationDbContext _context;
        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProductViewModel Do(int id) =>
            _context.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.ToString("N2"),
            })
            .FirstOrDefault();

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }
    }
   
}
