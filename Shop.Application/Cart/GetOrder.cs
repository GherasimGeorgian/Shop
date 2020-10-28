using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetOrder
    {
        private ISession _session;
        private ApplicationDbContext _context;
        public GetOrder(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }
        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
        }

        public class Product
        {
        
            public int ProductId { get; set; }

            public int Qty { get; set; }

            public int StockId { get; set; }
            public int Value { get; set; }

        }
        public class CustomerInformation
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
        public bool existInList(int x, List<CartProduct> cartList)
        {
            return true;
        }

        public Response Do()
        {
            var cart = _session.GetString("cart");



            List<CartProduct> cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var stockIds = cartList.Select(x => x.StockId);

            var listOfProducts = _context.Stocks
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int)(x.Product.Value * 100),
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();

            var customerInformationString = _session.GetString("customer-info");

            var customerInformation = JsonConvert.DeserializeObject<Shop.Domain.Models.CustomerInformation>(customerInformationString);

            return new Response 
            { 
               Products = listOfProducts,
               CustomerInformation = new CustomerInformation 
               {
                   FirstName = customerInformation.FirstName,
                   LastName = customerInformation.LastName,
                   Email = customerInformation.Email,
                   PhoneNumber = customerInformation.PhoneNumber,
                   Address1 = customerInformation.Address1,
                   Address2 = customerInformation.Address2,
                   City = customerInformation.City,
                   PostCode = customerInformation.PostCode,
               }
            };
        }
    }
}
