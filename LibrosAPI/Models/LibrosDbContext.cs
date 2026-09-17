// Data/LibrosDbContext.cs
using Microsoft.EntityFrameworkCore;
using LibrosAPI.Models;

public class LibrosDbContext : DbContext
{
    public LibrosDbContext(DbContextOptions<LibrosDbContext> options) : base(options) { }

    public DbSet<Libro> Libros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Libro>().HasData(
            new Libro { Id = 1, Titulo = "El Imperio Final", Autor = "Brandon Sanderson", Genero = "Fantasía", AnioPublicacion = 2006 },
            new Libro { Id = 2, Titulo = "Dune", Autor = "Frank Herbert", Genero = "Ciencia Ficción", AnioPublicacion = 1965 }
            
        );
    }
}