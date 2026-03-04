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
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly OrderManagementDbContext _context;
        private readonly IMapper _mapper;

        public ProductsControllerTests()
        {
            var options = new DbContextOptionsBuilder<OrderManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductsTestDb")
                .Options;

            _context = new OrderManagementDbContext(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _controller = new ProductsController(_context, _mapper);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var result = await _controller.GetProductById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedProduct()
        {
            var dto = new ProductDto { Name = "Laptop", Price = 1200 };

            var result = await _controller.PostProduct(dto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            var returnDto = Assert.IsType<ProductDto>(createdResult.Value);
            Assert.Equal("Laptop", returnDto.Name);
            Assert.NotEqual(0, returnDto.Id);
        }

        [Fact]
        public async Task DeleteProduct_RemovesProduct()
        {
            var product = new Product { Name = "Mouse", Price = 25, Description = "Top Product" };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteProduct(product.Id);
            Assert.IsType<NoContentResult>(result);

            Assert.Null(await _context.Products.FindAsync(product.Id));
        }
    }
}
