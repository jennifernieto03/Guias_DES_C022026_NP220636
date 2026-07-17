
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ejercicio.Models;
using Ejercicio.Data;

public class AsignacionController : Controller
{
    private readonly EjercicioDbContext _context;

    public AsignacionController(EjercicioDbContext context)
    {
        _context = context;
    }

    // GET: ASIGNACIONS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Asignaciones.ToListAsync());
    }

    // GET: ASIGNACIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var asignacion = await _context.Asignaciones
            .FirstOrDefaultAsync(m => m.Id == id);
        if (asignacion == null)
        {
            return NotFound();
        }

        return View(asignacion);
    }

    // GET: ASIGNACIONS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ASIGNACIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,EmpleadoId,Empleado,ProyectoId,Proyecto,Rol")] Asignacion asignacion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(asignacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(asignacion);
    }

    // GET: ASIGNACIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var asignacion = await _context.Asignaciones.FindAsync(id);
        if (asignacion == null)
        {
            return NotFound();
        }
        return View(asignacion);
    }

    // POST: ASIGNACIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,EmpleadoId,Empleado,ProyectoId,Proyecto,Rol")] Asignacion asignacion)
    {
        if (id != asignacion.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(asignacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(asignacion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(asignacion);
    }

    // GET: ASIGNACIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var asignacion = await _context.Asignaciones
            .FirstOrDefaultAsync(m => m.Id == id);
        if (asignacion == null)
        {
            return NotFound();
        }

        return View(asignacion);
    }

    // POST: ASIGNACIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var asignacion = await _context.Asignaciones.FindAsync(id);
        if (asignacion != null)
        {
            _context.Asignaciones.Remove(asignacion);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AsignacionExists(int? id)
    {
        return _context.Asignaciones.Any(e => e.Id == id);
    }
}
