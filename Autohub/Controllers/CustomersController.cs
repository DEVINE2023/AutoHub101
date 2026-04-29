using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ GET ALL CUSTOMERS WITH VEHICLES
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        // fetch customers from database
        var customers = await _context.Customers
            .Include(c => c.Vehicles)
            .ToListAsync();

        return Ok(customers);
    }

    // ✅ REGISTER CUSTOMER WITH VEHICLE
    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] Customer customer)
    {
        // store data in database
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Customer registered successfully",
            id = customer.Id
        });
    }

    // ✅ GET CUSTOMER BY ID (WITH VEHICLES)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        // fetch customer by id from database
        var customer = await _context.Customers
            .Include(c => c.Vehicles)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
        {
            return NotFound(new { message = "Customer not found" });
        }

        return Ok(customer);
    }

    // ✅ SEARCH CUSTOMERS (NAME / PHONE / VEHICLE NUMBER)
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        // search from database
        var customers = await _context.Customers
            .Include(c => c.Vehicles)
            .Where(c =>
                c.Name.Contains(term) ||
                c.Phone.Contains(term) ||
                c.Vehicles.Any(v => v.VehicleNumber.Contains(term))
            )
            .ToListAsync();

        return Ok(customers);
    }
}