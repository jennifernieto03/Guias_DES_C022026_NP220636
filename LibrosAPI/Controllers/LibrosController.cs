using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrosAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class LibrosController : ControllerBase
{
    private readonly LibrosDbContext _context;
    public LibrosController(LibrosDbContext context)
    {
        _context = context;
    }

    // GET: api/Libro
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Libro>>> GetLibro()
    {
        return await _context.Libros.ToListAsync();
    }

    // GET: api/Libro/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Libro>> GetLibro(int id)
    {
        var libro = await _context.Libros.FindAsync(id);

        if (libro == null)
        {
            return NotFound();
        }

        return libro;
    }

    // PUT: api/Libro/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLibro(int? id, Libro libro)
    {
        if (id != libro.Id)
        {
            return BadRequest();
        }

        _context.Entry(libro).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LibroExists(id))
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

    // POST: api/Libro
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Libro>> PostLibro(Libro libro)
    {
        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
    }

    // DELETE: api/Libro/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLibro(int? id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro == null)
        {
            return NotFound();
        }

        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LibroExists(int? id)
    {
        return _context.Libros.Any(e => e.Id == id);
    }
}
