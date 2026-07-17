using Microsoft.EntityFrameworkCore;
using Ejercicio.Models; // Asegúrate de incluir esta línea

namespace Ejercicio.Data
{
    public class EjercicioDbContext : DbContext
    {
        public EjercicioDbContext(DbContextOptions<EjercicioDbContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
    }
}
