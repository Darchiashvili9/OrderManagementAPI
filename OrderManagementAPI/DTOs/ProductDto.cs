using System.ComponentModel.DataAnnotations;

namespace OrderManagementAPI.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0, 1000)]
        public decimal Price { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}