using Microsoft.EntityFrameworkCore;
using OrderSystem.Models;

namespace OrderSystem.Data;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(b =>
        {
            b.HasKey(c => c.Id);
        });

        modelBuilder.Entity<Product>(b =>
        {
            b.HasKey(p => p.Id);
        });

        modelBuilder.Entity<Order>(b =>
        {
            b.HasKey(o => o.Id);
            b.HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);
        });

        modelBuilder.Entity<OrderItem>(b =>
        {
            b.HasKey(oi => oi.Id);
            b.HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);
            b.HasOne(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.ProductId);
        });

        // Seed data
        var customers = new List<Customer>
        {
            new Customer { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com" },
            new Customer { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob@example.com" },
            new Customer { Id = 3, FirstName = "Carol", LastName = "Davis", Email = "carol@example.com" },
            new Customer { Id = 4, FirstName = "David", LastName = "Miller", Email = "david@example.com" },
            new Customer { Id = 5, FirstName = "Eve", LastName = "Wilson", Email = "eve@example.com" }
        };

        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Widget A", Description = "Basic widget", Price = 9.99m },
            new Product { Id = 2, Name = "Widget B", Description = "Advanced widget", Price = 19.99m },
            new Product { Id = 3, Name = "Gadget C", Description = "Useful gadget", Price = 14.50m },
            new Product { Id = 4, Name = "Gadget D", Description = "Another gadget", Price = 24.00m },
            new Product { Id = 5, Name = "Thingamajig", Description = "Handy thing", Price = 4.75m },
            new Product { Id = 6, Name = "Doodad", Description = "Small doodad", Price = 7.30m },
            new Product { Id = 7, Name = "Whatsit", Description = "Mysterious whatsit", Price = 12.00m },
            new Product { Id = 8, Name = "Doohickey", Description = "Complex doohickey", Price = 29.99m },
            new Product { Id = 9, Name = "Contraption", Description = "Multi-part contraption", Price = 49.99m },
            new Product { Id = 10, Name = "Gizmo", Description = "Handheld gizmo", Price = 5.25m }
        };

        var orders = new List<Order>
        {
            new Order { Id = 1, CustomerId = 1, OrderDate = new DateTime(2023,1,1), Total = 29.98m },
            new Order { Id = 2, CustomerId = 1, OrderDate = new DateTime(2023,1,3), Total = 19.99m },
            new Order { Id = 3, CustomerId = 2, OrderDate = new DateTime(2023,1,4), Total = 14.5m },
            new Order { Id = 4, CustomerId = 3, OrderDate = new DateTime(2023,1,5), Total = 4.75m },
            new Order { Id = 5, CustomerId = 4, OrderDate = new DateTime(2023,1,6), Total = 57.99m },
            new Order { Id = 6, CustomerId = 5, OrderDate = new DateTime(2023,1,7), Total = 12.0m },
            new Order { Id = 7, CustomerId = 2, OrderDate = new DateTime(2023,1,8), Total = 9.99m },
            new Order { Id = 8, CustomerId = 3, OrderDate = new DateTime(2023,1,9), Total = 24.0m },
            new Order { Id = 9, CustomerId = 4, OrderDate = new DateTime(2023,1,10), Total = 74.99m },
            new Order { Id = 10, CustomerId = 5, OrderDate = new DateTime(2023,1,11), Total = 22.5m }
        };

        var items = new List<OrderItem>
        {
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 9.99m },
            new OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 19.99m },
            new OrderItem { Id = 3, OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 14.5m },
            new OrderItem { Id = 4, OrderId = 4, ProductId = 5, Quantity = 1, UnitPrice = 4.75m },
            new OrderItem { Id = 5, OrderId = 5, ProductId = 8, Quantity = 1, UnitPrice = 29.99m },
            new OrderItem { Id = 6, OrderId = 5, ProductId = 6, Quantity = 4, UnitPrice = 6.0m },
            new OrderItem { Id = 7, OrderId = 6, ProductId = 7, Quantity = 1, UnitPrice = 12.0m },
            new OrderItem { Id = 8, OrderId = 7, ProductId = 1, Quantity = 1, UnitPrice = 9.99m },
            new OrderItem { Id = 9, OrderId = 8, ProductId = 4, Quantity = 1, UnitPrice = 24.0m },
            new OrderItem { Id = 10, OrderId = 9, ProductId = 9, Quantity = 1, UnitPrice = 49.99m },
            new OrderItem { Id = 11, OrderId = 9, ProductId = 10, Quantity = 1, UnitPrice = 24.999m },
            new OrderItem { Id = 12, OrderId = 10, ProductId = 3, Quantity = 1, UnitPrice = 14.5m },
            new OrderItem { Id = 13, OrderId = 10, ProductId = 10, Quantity = 1, UnitPrice = 8.0m }
        };

        modelBuilder.Entity<Customer>().HasData(customers);
        modelBuilder.Entity<Product>().HasData(products);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<OrderItem>().HasData(items);

        base.OnModelCreating(modelBuilder);
    }

    // Note: OnConfiguring fallback removed to ensure DbContext is always configured through DI with a single connection string.
}
