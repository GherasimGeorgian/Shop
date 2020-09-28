using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Shop.UI.Controllers;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Tests
{
    [TestClass]
    public class AdminControllerTest
    {
        ServiceCollection services;
        ApplicationDbContext context;
        public AdminControllerTest()
        {
            services = new ServiceCollection();

            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            context = new ApplicationDbContext(
                builder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyShop;MultipleActiveResultSets=true;Integrated Security = False;")
                .Options);
        }

      
        [TestMethod]
        public void TestMethod1()
        {

            //// Arrange

            //AdminController adminController = new AdminController(context);

          
            //// Act

            //adminController.GetProduct(0);

            //// Assert

        }
        [TestMethod]
        public void TestGetProductAdmin()
        {
            // Arrange
            Shop.Application.ProductsAdmin.GetProduct getProduct = new Application.ProductsAdmin.GetProduct(context);

            // Act
            Shop.Application.ProductsAdmin.GetProduct.ProductViewModel result = getProduct.Do(2);
            Shop.Application.ProductsAdmin.GetProduct.ProductViewModel resultNull = getProduct.Do(0);

            // Assert
            Assert.AreEqual(result.Id,2);
            Assert.IsNull(resultNull);

        }
        [TestMethod]
        public void TestGetProductsAdmin()
        {
            // Arrange
            Shop.Application.ProductsAdmin.GetProducts getProducts = new Application.ProductsAdmin.GetProducts(context);

            // Act
            IEnumerable<Shop.Application.ProductsAdmin.GetProducts.ProductViewModel> result = getProducts.Do();
          
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Count(),0);

        }

        [TestMethod]
        public async Task TestCreateDeleteProductAdmin()
        {
            // Arrange
            Shop.Application.ProductsAdmin.GetProducts getProducts = new Application.ProductsAdmin.GetProducts(context);
            Shop.Application.ProductsAdmin.CreateProduct createProduct = new Application.ProductsAdmin.CreateProduct(context);
            Shop.Application.ProductsAdmin.DeleteProduct deleteProduct = new Application.ProductsAdmin.DeleteProduct(context);

            // Act
            IEnumerable<Shop.Application.ProductsAdmin.GetProducts.ProductViewModel> productsBefore = getProducts.Do();

            int NOProductsBefore = productsBefore.Count();

            Shop.Application.ProductsAdmin.CreateProduct.Request request = new Application.ProductsAdmin.CreateProduct.Request();

            request.Name = "product_test";
            request.Description = "description_test";
            request.Value = "11.12";

            var responseCreate = await createProduct.Do(request);




            // Assert
            Assert.IsNotNull(responseCreate);
            Assert.AreEqual(responseCreate.Name, "product_test");
            Assert.AreEqual(responseCreate.Description, "description_test");
            Assert.AreEqual(responseCreate.Value, decimal.Parse("11.12"));


            var resultDelete  =  await deleteProduct.Do(responseCreate.Id);
            Assert.IsTrue(resultDelete);


            
            IEnumerable<Shop.Application.ProductsAdmin.GetProducts.ProductViewModel> productsAfter = getProducts.Do();
            int NOProductsAfter = productsAfter.Count();


            Assert.AreEqual(NOProductsBefore, NOProductsAfter);
        }
        [TestMethod]
        public async Task TestUpdateProductAdmin()
        {
            // Arrange
            Shop.Application.ProductsAdmin.UpdateProduct updateProduct = new Application.ProductsAdmin.UpdateProduct(context);

            // Act

            Shop.Application.ProductsAdmin.UpdateProduct.Request request = new Application.ProductsAdmin.UpdateProduct.Request();

            request.Id = 2;
            request.Name = "marfaweda";
            request.Description = "balabada";
            request.Value = "22.41";

            var responseUpdate = await updateProduct.Do(request);

            //Assert
            Assert.AreEqual(responseUpdate.Id,request.Id);
            Assert.AreEqual(responseUpdate.Name,request.Name);
            Assert.AreEqual(responseUpdate.Description,request.Description);
            Assert.AreEqual(responseUpdate.Value, decimal.Parse(request.Value));

        }
    }
}
