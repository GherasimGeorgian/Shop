using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Shop.UI.Controllers;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shop.Database;

namespace Shop.Tests
{
    [TestClass]
    public class AdminControllerTest
    {

        [TestMethod]
        public void TestMethod1()
        {

            // Arrange

            var services = new ServiceCollection();

            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var context = new ApplicationDbContext(
                builder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyShop;MultipleActiveResultSets=true;Integrated Security = False;")
                .Options);


            AdminController adminController = new AdminController(context);

           

            // Act

            adminController.GetProduct(0);

            // Assert

        }
        [TestMethod]
        public void TestGetProductAdmin()
        {
            var services = new ServiceCollection();

            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var context = new ApplicationDbContext(
                builder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyShop;MultipleActiveResultSets=true;Integrated Security = False;")
                .Options);

            Shop.Application.ProductsAdmin.GetProduct getProduct = new Application.ProductsAdmin.GetProduct(context);
            Shop.Application.ProductsAdmin.GetProduct.ProductViewModel result = getProduct.Do(0);
            Shop.Application.ProductsAdmin.GetProduct.ProductViewModel x = new Application.ProductsAdmin.GetProduct.ProductViewModel();
          //  Assert(result.class == Shop.Application.ProductsAdmin.GetProduct.ProductViewModel.class);
            Assert.AreEqual(result.GetType(), x.GetType());
        }
    }
}
