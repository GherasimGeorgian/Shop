using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private ApplicationDbContext _context;

        public DeleteStock(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Do(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            _context.Stocks.Remove(stock);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
