namespace OrderSystem.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Order>? Orders { get; set; }
}
