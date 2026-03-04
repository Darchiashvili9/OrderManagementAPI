using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Controllers;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;
using OrderManagementAPI.Profiles;

namespace OrderManagementAPI.Tests
{
    public class CustomersControllerTests
    {
        private readonly CustomersController _controller;
        private readonly OrderManagementDbContext _context;
        private readonly IMapper _mapper;

        public CustomersControllerTests()
        {
            var options = new DbContextOptionsBuilder<OrderManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new OrderManagementDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _controller = new CustomersController(_context, _mapper);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            var result = await _controller.GetCustomerById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCustomer_ReturnsCreatedCustomer()
        {
            var dto = new CustomerDto { Name = "Test User", Email = "test@example.com" };

            var result = await _controller.PostCustomer(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            var returnDto = Assert.IsType<CustomerDto>(createdResult.Value);
            Assert.Equal("Test User", returnDto.Name);
            Assert.NotEqual(0, returnDto.Id);
        }

        [Fact]
        public async Task DeleteCustomer_RemovesCustomer()
        {
            var customer = new Customer { Name = "Delete Me", Email = "delete@example.com" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteCustomer(customer.Id);
            Assert.IsType<NoContentResult>(result);

            Assert.Null(await _context.Customers.FindAsync(customer.Id));
        }
    }

}
