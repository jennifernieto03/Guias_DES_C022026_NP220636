using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ProductosDbContext _context;
    private readonly IConnectionMultiplexer _redis;
    public ProductosController(
        ProductosDbContext context,
        IConnectionMultiplexer redis
        )
    {
        _context = context;
        _redis = redis;
    }

    // GET: api/Productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        // Crea la instancia de la base de datos de Redis
        var dbRedis = _redis.GetDatabase();
        // Define la clave para almacenar la lista de productos
        var cacheKey = "productos_list";
        // Obtiene la lista de productos desde la cache
        var productosCache = await _redis.GetDatabase().StringGetAsync(cacheKey);
        // Si la lista no esta vacia se retorna desde la chace
        if (!productosCache.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<List<Producto>>(productosCache.ToString());
        }
        // Caso contrario, se obtienen los productos desde la base de datos
        var productos = await _context.Productos.AsNoTracking().ToListAsync();
        // Se almacena la lista obtenida en la cache por 10 minutos
        await dbRedis.StringSetAsync(cacheKey, JsonSerializer.Serialize(productos),
       TimeSpan.FromMinutes(10));
        return productos;
    }

    // GET: api/Productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var dbRedis = _redis.GetDatabase();
        var cacheKey = $"producto_{id}";
        var productoChace = await _redis.GetDatabase().StringGetAsync(cacheKey);
        if (!productoChace.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<Producto>(productoChace.ToString());
        }
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        await dbRedis.StringSetAsync(cacheKey, JsonSerializer.Serialize(producto),
       TimeSpan.FromMinutes(10));
        return producto;
    }

    // PUT: api/Productos/5
    // To protect from overposting attacks, see
    // https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, Producto producto)
    {
        if (id != producto.Id)
        {
            return BadRequest();
        }
        _context.Entry(producto).State = EntityState.Modified;
        try
        {
            var dbRedis = _redis.GetDatabase();
            var cacheKeyProducto = $"producto_{id}";
            var cacheKeyLista = "productos_list";
            // Elimina el producto de la cache
            await dbRedis.KeyDeleteAsync(cacheKeyProducto);
            // Elimina la lista de productos de la cache
            await dbRedis.KeyDeleteAsync(cacheKeyLista);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(id))
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


    // POST: api/Productos
    // To protect from overposting attacks, see
    // https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        var dbRedis = _redis.GetDatabase();
        var cacheKeyLista = "productos_list";
        await dbRedis.KeyDeleteAsync(cacheKeyLista);
        return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
    }

    // DELETE: api/Productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        var dbRedis = _redis.GetDatabase();
        var cacheKeyProducto = $"producto_{id}";
        var cacheKeyLista = "productos_list";
        await dbRedis.KeyDeleteAsync(cacheKeyProducto);
        await dbRedis.KeyDeleteAsync(cacheKeyLista);
        return NoContent();
    }


    private bool ProductoExists(int? id)
    {
        return _context.Productos.Any(e => e.Id == id);
    }
}
