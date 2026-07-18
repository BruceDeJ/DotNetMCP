using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Models;

namespace OrderSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderContext _db;

    public OrdersController(OrderContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _db.Orders
            .Include(o => o.Items)
                .ThenInclude(x => x.Product)
            .Include(o => o.Customer)
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(int id)
    {
        var o = await _db.Orders
            .Include(x => x.Items).ThenInclude(i => i.Product)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (o == null) return NotFound();
        return o;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Create(Order order)
    {
        // Calculate total
        if (order.Items != null)
        {
            order.Total = order.Items.Sum(i => i.UnitPrice * i.Quantity);
            foreach (var item in order.Items)
            {
                item.Product = null; // avoid EF trying to create product
            }
        }
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order order)
    {
        if (id != order.Id) return BadRequest();
        // Update order header
        var existing = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        if (existing == null) return NotFound();

        existing.OrderDate = order.OrderDate;
        existing.CustomerId = order.CustomerId;
        // Replace items
        if (order.Items != null)
        {
            _db.OrderItems.RemoveRange(existing.Items ?? Enumerable.Empty<OrderItem>());
            foreach (var item in order.Items)
            {
                item.Id = 0; // ensure new
                existing.Items ??= new List<OrderItem>();
                existing.Items.Add(item);
            }
            existing.Total = existing.Items.Sum(i => i.UnitPrice * i.Quantity);
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _db.Orders.FindAsync(id);
        if (existing == null) return NotFound();
        _db.Orders.Remove(existing);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
