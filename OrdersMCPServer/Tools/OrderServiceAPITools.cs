using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ModelContextProtocol.Server;
using OrdersMCPServer.Models;
using OrdersMCPServer.OrderSystem.Client;
using System.ComponentModel;

/// <summary>
/// Tools for the Order Service API
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
internal class OrderServiceAPITools
{
    readonly OrderServiceClient _orderServiceClient;
    readonly ILogger<OrderServiceAPITools> _logger;

    public OrderServiceAPITools(OrderServiceClient orderServiceClient, ILogger<OrderServiceAPITools> logger)
    {
        _orderServiceClient = orderServiceClient;
        _logger = logger;
    }

    [McpServerTool]
    [Description("Retrieves a list of all customers.")]
    public async Task<List<Customer>> GetCustomers()
    {
        try
        {
            var res = await _orderServiceClient.GetCustomersAsync();
            return res ?? new List<Customer>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving customers.");
            throw new ApplicationException("An error occurred while retrieving customers.", ex);
        }
    }

    [McpServerTool]
    [Description("Retrieves a customer by their ID.")]
    public async Task<Customer> GetCustomerById(int id)
    {
        try
        {
            var res = await _orderServiceClient.GetCustomerByIdAsync(id);
            if (res == null) throw new ApplicationException($"Customer with id {id} not found.");
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving customers.");
            throw new ApplicationException("An error occurred while retrieving customers.", ex);
        }
    }

    [McpServerTool]
    [Description("Creates a new customer.")]
    public async Task<Customer> CreateCustomer(Customer customer)
    {
        try
        {
            var res = await _orderServiceClient.CreateCustomerAsync(customer);
            if (res == null) throw new ApplicationException("Failed to create customer.");
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a customer.");
            throw new ApplicationException("An error occurred while creating a customer.", ex);
        }
    }

    [McpServerTool]
    [Description("Updates an existing customer.")]
    public async Task UpdateCustomer(int id, Customer customer)
    {
        try
        {
            await _orderServiceClient.UpdateCustomerAsync(id, customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a customer.");
            throw new ApplicationException("An error occurred while updating a customer.", ex);
        }
    }

    [McpServerTool]
    [Description("Deletes a customer by ID.")]
    public async Task DeleteCustomer(int id)
    {
        try
        {
            await _orderServiceClient.DeleteCustomerAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a customer.");
            throw new ApplicationException("An error occurred while deleting a customer.", ex);
        }
    }

    // Products
    [McpServerTool]
    [Description("Retrieves a list of all products.")]
    public async Task<List<Product>> GetProducts()
    {
        try
        {
            var res = await _orderServiceClient.GetProductsAsync();
            return res ?? new List<Product>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving products.");
            throw new ApplicationException("An error occurred while retrieving products.", ex);
        }
    }

    [McpServerTool]
    [Description("Retrieves a product by ID.")]
    public async Task<Product> GetProductById(int id)
    {
        try
        {
            var res = await _orderServiceClient.GetProductByIdAsync(id);
            if (res == null) throw new ApplicationException($"Product with id {id} not found.");
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving product.");
            throw new ApplicationException("An error occurred while retrieving product.", ex);
        }
    }

    [McpServerTool]
    [Description("Creates a new product.")]
    public async Task<Product> CreateProduct(Product product)
    {
        try
        {
            var res = await _orderServiceClient.CreateProductAsync(product);
            if (res == null) throw new ApplicationException("Failed to create product.");
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating product.");
            throw new ApplicationException("An error occurred while creating product.", ex);
        }
    }

    [McpServerTool]
    [Description("Updates an existing product.")]
    public async Task UpdateProduct(int id, Product product)
    {
        try
        {
            await _orderServiceClient.UpdateProductAsync(id, product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating product.");
            throw new ApplicationException("An error occurred while updating product.", ex);
        }
    }

    [McpServerTool]
    [Description("Deletes a product by ID.")]
    public async Task DeleteProduct(int id)
    {
        try
        {
            await _orderServiceClient.DeleteProductAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting product.");
            throw new ApplicationException("An error occurred while deleting product.", ex);
        }
    }

    // Orders
    [McpServerTool]
    [Description("Retrieves a list of all orders.")]
    public async Task<List<Order>> GetOrders()
    {
        try
        {
            var res = await _orderServiceClient.GetOrdersAsync();
            return res ?? new List<Order>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving orders.");
            throw new ApplicationException("An error occurred while retrieving orders.", ex);
        }
    }

    [McpServerTool]
    [Description("Retrieves an order by ID.")]
    public async Task<Order> GetOrderById(int id)
    {
        try
        {
            var res = await _orderServiceClient.GetOrderByIdAsync(id);
            if (res == null) throw new ApplicationException($"Order with id {id} not found.");
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving order.");
            throw new ApplicationException("An error occurred while retrieving order.", ex);
        }
    }

    [McpServerTool]
    [Description("Creates a new order.")]
    public async Task<Order> CreateOrder(Order order)
    {
        try
        {
            var res = await _orderServiceClient.CreateOrderAsync(order);
            if (res == null) throw new ApplicationException("Failed to create order.");
            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating order.");
            throw new ApplicationException("An error occurred while creating order.", ex);
        }
    }

    [McpServerTool]
    [Description("Updates an existing order.")]
    public async Task UpdateOrder(int id, Order order)
    {
        try
        {
            await _orderServiceClient.UpdateOrderAsync(id, order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating order.");
            throw new ApplicationException("An error occurred while updating order.", ex);
        }
    }

    [McpServerTool]
    [Description("Deletes an order by ID.")]
    public async Task DeleteOrder(int id)
    {
        try
        {
            await _orderServiceClient.DeleteOrderAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting order.");
            throw new ApplicationException("An error occurred while deleting order.", ex);
        }
    }
}
