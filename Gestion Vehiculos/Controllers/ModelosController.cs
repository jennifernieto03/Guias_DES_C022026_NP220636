using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Vehiculos.Models;
using Gestion_Vehiculos.Data;

[Route("api/[controller]")]
[ApiController]
public class ModelosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ModelosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Modelo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Modelo>>> GetModelo()
    {
        return await _context.Modelos.ToListAsync();
    }

    // GET: api/Modelo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Modelo>> GetModelo(int id)
    {
        var modelo = await _context.Modelos.FindAsync(id);

        if (modelo == null)
        {
            return NotFound();
        }

        return modelo;
    }

    // PUT: api/Modelo/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutModelo(int? id, Modelo modelo)
    {
        if (id != modelo.Id)
        {
            return BadRequest();
        }

        _context.Entry(modelo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ModeloExists(id))
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

    // POST: api/Modelo
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Modelo>> PostModelo(Modelo modelo)
    {
        _context.Modelos.Add(modelo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetModelo", new { id = modelo.Id }, modelo);
    }

    // DELETE: api/Modelo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModelo(int? id)
    {
        var modelo = await _context.Modelos.FindAsync(id);
        if (modelo == null)
        {
            return NotFound();
        }

        _context.Modelos.Remove(modelo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ModeloExists(int? id)
    {
        return _context.Modelos.Any(e => e.Id == id);
    }
}
