namespace OrderManagementAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";

        public Customer? Customer { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
