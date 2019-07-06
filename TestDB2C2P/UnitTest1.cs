using CustomerService.Controllers;
using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestDB2C2P
{
    public class UnitTest1
    {
        
        [Fact]
        public async Task TestGetDataByIdAsync()
        {
            var dbContext = DbContextMocker.GetMockedDB2C2PContext(nameof(TestGetDataByIdAsync));
            var controller = new CustomersController(dbContext);

            var response = await controller.PostCustomer(new CustomerRequest { CustomerId = 1 });
            var value = response.Value as Customer;
            {
                Assert.Equal("beer1@gmail.com", value.ContactEmail);
                Assert.Equal("0811111111", value.MobileNo);
            }

            var response2 = await controller.PostCustomer(new CustomerRequest { CustomerId = 2 });
            var value2 = response2.Value as Customer;
            {
                Assert.Equal("beer2@gmail.com", value2.ContactEmail);
                Assert.Equal("0822222222", value2.MobileNo);
            }

            dbContext.Dispose();

            // #1 request
            
            //Assert.Equal(3, value.Transactions.Count);  // transaction is blank!!
        }
        
        
        [Fact]
        public async Task TestGetDataByEmailAsync()
        {
            var dbContext = DbContextMocker.GetMockedDB2C2PContext(nameof(TestGetDataByEmailAsync));
            var controller = new CustomersController(dbContext);

            var response = await controller.PostCustomer(new CustomerRequest { Email = "beer2@gmail.com" });

            Assert.NotNull(response.Value);
            var value = response.Value as Customer;
            {
                Assert.Equal(2, value.CustomerId);
                Assert.Equal("0822222222", value.MobileNo);
            }
            
            dbContext.Dispose();
        }
        [Fact]
        public async Task TestError1Async()
        {
            var dbContext = DbContextMocker.GetMockedDB2C2PContext(nameof(TestError1Async));
            var controller = new CustomersController(dbContext);

            var response = await controller.PostCustomer(new CustomerRequest());

            Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal("No inquiry criteria", ((BadRequestObjectResult)response.Result).Value);

            response = await controller.PostCustomer(new CustomerRequest { Email = "abcd" });
            Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal("Invalid email", ((BadRequestObjectResult)response.Result).Value);

            dbContext.Dispose();
        }
    }
}
