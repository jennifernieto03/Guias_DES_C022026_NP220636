using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using LibrosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace LibrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibrosDbContext _context;
        private readonly IConnectionMultiplexer _redis;

        // Se inyectan el contexto de base de datos y la conexión a Redis[cite: 1]
        public LibrosController(
            LibrosDbContext context,
            IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            // Crea la instancia de la base de datos de Redis[cite: 1]
            var dbRedis = _redis.GetDatabase();

            // Define la clave para almacenar la lista de libros[cite: 1]
            var cacheKey = "libros_list";

            // Obtiene la lista de libros desde la caché[cite: 1]
            var librosCache = await dbRedis.StringGetAsync(cacheKey);

            // Si la lista no está vacía se retorna desde la caché[cite: 1]
            if (!librosCache.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<List<Libro>>(librosCache.ToString());
            }

            // Caso contrario, se obtienen los libros desde la base de datos[cite: 1]
            var libros = await _context.Libros.AsNoTracking().ToListAsync();

            // Se almacena la lista obtenida en la caché por 10 minutos[cite: 1]
            await dbRedis.StringSetAsync(cacheKey, JsonSerializer.Serialize(libros), TimeSpan.FromMinutes(10));

            return libros;
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            // Se especifica el id del elemento para la caché[cite: 1]
            var cacheKey = $"libro_{id}";
            var dbRedis = _redis.GetDatabase();
            var libroCache = await dbRedis.StringGetAsync(cacheKey);

            if (!libroCache.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<Libro>(libroCache.ToString());
            }

            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            await dbRedis.StringSetAsync(cacheKey, JsonSerializer.Serialize(libro), TimeSpan.FromMinutes(10));

            return libro;
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                var dbRedis = _redis.GetDatabase();
                var cacheKeyLibro = $"libro_{id}";
                var cacheKeyLista = "libros_list";

                // Se elimina el libro específico y la lista completa de la caché para garantizar información actualizada[cite: 1]
                await dbRedis.KeyDeleteAsync(cacheKeyLibro);
                await dbRedis.KeyDeleteAsync(cacheKeyLista);

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

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            // Nueva validación
            if (string.IsNullOrEmpty(libro.Titulo))
            {
                return BadRequest("El libro no tiene título.");
            }
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            var dbRedis = _redis.GetDatabase();
            var cacheKeyLibro = $"libro_{id}";
            var cacheKeyLista = "libros_list";

            // Se elimina la lista y el objeto individual para que no se muestren en siguientes consultas[cite: 1]
            await dbRedis.KeyDeleteAsync(cacheKeyLibro);
            await dbRedis.KeyDeleteAsync(cacheKeyLista);

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}