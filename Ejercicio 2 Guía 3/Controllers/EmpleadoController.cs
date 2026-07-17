
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ejercicio.Models;
using Ejercicio.Data;

public class EmpleadoController : Controller
{
    private readonly EjercicioDbContext _context;

    public EmpleadoController(EjercicioDbContext context)
    {
        _context = context;
    }

    // GET: EMPLEADOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Empleados.ToListAsync());
    }

    // GET: EMPLEADOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empleado = await _context.Empleados
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empleado == null)
        {
            return NotFound();
        }

        return View(empleado);
    }

    // GET: EMPLEADOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EMPLEADOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,FechaContratacion,Puesto,Asignaciones")] Empleado empleado)
    {
        if (ModelState.IsValid)
        {
            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(empleado);
    }

    // GET: EMPLEADOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }
        return View(empleado);
    }

    // POST: EMPLEADOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,FechaContratacion,Puesto,Asignaciones")] Empleado empleado)
    {
        if (id != empleado.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(empleado);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(empleado.Id))
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
        return View(empleado);
    }

    // GET: EMPLEADOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empleado = await _context.Empleados
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empleado == null)
        {
            return NotFound();
        }

        return View(empleado);
    }

    // POST: EMPLEADOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado != null)
        {
            _context.Empleados.Remove(empleado);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmpleadoExists(int? id)
    {
        return _context.Empleados.Any(e => e.Id == id);
    }
}
