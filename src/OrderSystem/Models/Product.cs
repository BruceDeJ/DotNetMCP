namespace OrderSystem.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<OrderItem>? OrderItems { get; set; }
}
