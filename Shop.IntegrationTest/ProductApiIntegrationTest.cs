using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.IntegrationTest
{
    public class ProductApiIntegrationTest
    {
        [Fact]
        public async Task Test_Get_All()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("Admin");
                 
                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
               
            }
        }

        [Fact]
        public async Task Test_Post()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("Admin"
               , new StringContent(
               JsonConvert.SerializeObject(new Shop.Application.ProductsAdmin.CreateProduct.Request()
               {
                   Name = "caca",
                   Description = "caca desc",
                   Value = "11.22"
               }),
           Encoding.UTF8,
           "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
    }
}
