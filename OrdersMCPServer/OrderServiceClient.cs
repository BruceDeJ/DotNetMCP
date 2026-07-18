namespace OrdersMCPServer
{
    using OrdersMCPServer.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading;
    using System.Threading.Tasks;

    namespace OrderSystem.Client
    {
        public class OrderServiceClient
        {
            private readonly HttpClient _httpClient;

            public OrderServiceClient(HttpClient httpClient)
            {
                _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            }

            #region Customers API

            public async Task<List<Customer>?> GetCustomersAsync(CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customers", cancellationToken);
            }

            public async Task<Customer?> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PostAsJsonAsync("api/Customers", customer, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Customer>(cancellationToken: cancellationToken);
            }

            public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<Customer>($"api/Customers/{id}", cancellationToken);
            }

            public async Task UpdateCustomerAsync(int id, Customer customer, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Customers/{id}", customer, cancellationToken);
                response.EnsureSuccessStatusCode();
            }

            public async Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.DeleteAsync($"api/Customers/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();
            }

            #endregion

            #region Orders API

            public async Task<List<Order>?> GetOrdersAsync(CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<List<Order>>("api/Orders", cancellationToken);
            }

            public async Task<Order?> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PostAsJsonAsync("api/Orders", order, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Order>(cancellationToken: cancellationToken);
            }

            public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<Order>($"api/Orders/{id}", cancellationToken);
            }

            public async Task UpdateOrderAsync(int id, Order order, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Orders/{id}", order, cancellationToken);
                response.EnsureSuccessStatusCode();
            }

            public async Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.DeleteAsync($"api/Orders/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();
            }

            #endregion

            #region Products API

            public async Task<List<Product>?> GetProductsAsync(CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<List<Product>>("api/Products", cancellationToken);
            }

            public async Task<Product?> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PostAsJsonAsync("api/Products", product, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Product>(cancellationToken: cancellationToken);
            }

            public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<Product>($"api/Products/{id}", cancellationToken);
            }

            public async Task UpdateProductAsync(int id, Product product, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Products/{id}", product, cancellationToken);
                response.EnsureSuccessStatusCode();
            }

            public async Task DeleteProductAsync(int id, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.DeleteAsync($"api/Products/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();
            }

            #endregion
        }
    }
}
