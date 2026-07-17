
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ejercicio.Models;
using Ejercicio.Data;

public class ProyectoController : Controller
{
    private readonly EjercicioDbContext _context;

    public ProyectoController(EjercicioDbContext context)
    {
        _context = context;
    }

    // GET: PROYECTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Proyectos.ToListAsync());
    }

    // GET: PROYECTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proyecto = await _context.Proyectos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (proyecto == null)
        {
            return NotFound();
        }

        return View(proyecto);
    }

    // GET: PROYECTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PROYECTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,FechaInicio,Asignaciones")] Proyecto proyecto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(proyecto);
    }

    // GET: PROYECTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proyecto = await _context.Proyectos.FindAsync(id);
        if (proyecto == null)
        {
            return NotFound();
        }
        return View(proyecto);
    }

    // POST: PROYECTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Descripcion,FechaInicio,Asignaciones")] Proyecto proyecto)
    {
        if (id != proyecto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(proyecto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(proyecto.Id))
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
        return View(proyecto);
    }

    // GET: PROYECTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proyecto = await _context.Proyectos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (proyecto == null)
        {
            return NotFound();
        }

        return View(proyecto);
    }

    // POST: PROYECTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var proyecto = await _context.Proyectos.FindAsync(id);
        if (proyecto != null)
        {
            _context.Proyectos.Remove(proyecto);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProyectoExists(int? id)
    {
        return _context.Proyectos.Any(e => e.Id == id);
    }
}
