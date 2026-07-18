using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Models;

namespace OrderSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly OrderContext _db;

    public CustomersController(OrderContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _db.Customers.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> Get(int id)
    {
        var c = await _db.Customers.FindAsync(id);
        if (c == null) return NotFound();
        return c;
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Create(Customer customer)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Customer customer)
    {
        if (id != customer.Id) return BadRequest();
        _db.Entry(customer).State = EntityState.Modified;
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Customers.AnyAsync(c => c.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var c = await _db.Customers.FindAsync(id);
        if (c == null) return NotFound();
        _db.Customers.Remove(c);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
