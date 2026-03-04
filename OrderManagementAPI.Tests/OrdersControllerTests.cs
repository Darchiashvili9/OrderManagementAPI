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
    public class OrdersControllerTests
    {
        private readonly OrdersController _controller;
        private readonly OrderManagementDbContext _context;
        private readonly IMapper _mapper;

        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<OrderManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "OrdersTestDb")
                .Options;

            _context = new OrderManagementDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _controller = new OrdersController(_context, _mapper);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var result = await _controller.GetOrderById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostOrder_ReturnsCreatedOrder()
        {
            // Arrange: create customer + product first
            var customer = new Customer { Name = "Order User", Email = "order@example.com" };
            var product = new Product { Name = "Keyboard", Price = 50, Description = "Pfullin" };
            _context.Customers.Add(customer);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var dto = new OrderDto
            {
                CustomerId = customer.Id,
                Status = "Pending",
                Items = new List<OrderItemDto>
            {
                new OrderItemDto { ProductId = product.Id, Quantity = 2 }
            }
            };

            // Act
            var result = await _controller.PostOrder(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            var returnDto = Assert.IsType<OrderDto>(createdResult.Value);
            Assert.Equal(customer.Id, returnDto.CustomerId);
            Assert.Single(returnDto.Items);
        }

        [Fact]
        public async Task DeleteOrder_RemovesOrder()
        {
            var customer = new Customer { Name = "Delete Order User", Email = "deleteorder@example.com" };
            var product = new Product { Name = "Monitor", Price = 200, Description = "Pfullin" };
            _context.Customers.Add(customer);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var order = new Order
            {
                CustomerId = customer.Id,
                Items = new List<OrderItem>
            {
                new OrderItem { ProductId = product.Id, Quantity = 1 }
            }
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteOrder(order.Id);
            Assert.IsType<NoContentResult>(result);

            Assert.Null(await _context.Orders.FindAsync(order.Id));
        }
    }
}
