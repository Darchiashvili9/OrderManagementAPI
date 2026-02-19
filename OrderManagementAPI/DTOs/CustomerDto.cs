using System.ComponentModel.DataAnnotations;

namespace OrderManagementAPI.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}