using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.GetProducts
{
    public class GetProducts
    {
        private ApplicationDbContext _context;
        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> Do() =>
            _context.Products.ToList().Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = $"$ {x.Value.ToString("N2")}", // 1100.40 ---> 1,100.40 ---?> $1,100.40
            });
        
    }
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
