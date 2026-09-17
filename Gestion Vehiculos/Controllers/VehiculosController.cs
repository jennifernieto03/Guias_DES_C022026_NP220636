using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Vehiculos.Models;
using Gestion_Vehiculos.Data;

[Route("api/[controller]")]
[ApiController]
public class VehiculosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public VehiculosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Vehiculo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculo()
    {
        return await _context.Vehiculos.ToListAsync();
    }

    // GET: api/Vehiculo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);

        if (vehiculo == null)
        {
            return NotFound();
        }

        return vehiculo;
    }

    // PUT: api/Vehiculo/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehiculo(int? id, Vehiculo vehiculo)
    {
        if (id != vehiculo.Id)
        {
            return BadRequest();
        }

        _context.Entry(vehiculo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VehiculoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Vehiculo
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
    {
        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetVehiculo", new { id = vehiculo.Id }, vehiculo);
    }

    // DELETE: api/Vehiculo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehiculo(int? id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null)
        {
            return NotFound();
        }

        _context.Vehiculos.Remove(vehiculo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VehiculoExists(int? id)
    {
        return _context.Vehiculos.Any(e => e.Id == id);
    }
}
