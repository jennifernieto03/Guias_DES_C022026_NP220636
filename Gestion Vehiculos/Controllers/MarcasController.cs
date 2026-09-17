using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Vehiculos.Models;
using Gestion_Vehiculos.Data;

[Route("api/[controller]")]
[ApiController]
public class MarcasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public MarcasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Marca
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Marca>>> GetMarca()
    {
        return await _context.Marcas.ToListAsync();
    }

    // GET: api/Marca/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Marca>> GetMarca(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);

        if (marca == null)
        {
            return NotFound();
        }

        return marca;
    }

    // PUT: api/Marca/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMarca(int? id, Marca marca)
    {
        if (id != marca.Id)
        {
            return BadRequest();
        }

        _context.Entry(marca).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MarcaExists(id))
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

    // POST: api/Marca
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Marca>> PostMarca(Marca marca)
    {
        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMarca", new { id = marca.Id }, marca);
    }

    // DELETE: api/Marca/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarca(int? id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null)
        {
            return NotFound();
        }

        _context.Marcas.Remove(marca);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MarcaExists(int? id)
    {
        return _context.Marcas.Any(e => e.Id == id);
    }
}
