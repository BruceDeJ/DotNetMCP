using System.ComponentModel.DataAnnotations.Schema;

namespace OrderSystem.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }

    public ICollection<OrderItem>? Items { get; set; }
}
