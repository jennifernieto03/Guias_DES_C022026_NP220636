using Microsoft.EntityFrameworkCore;
using Registro_Personas_Naturales_NP220636.Models;

namespace Registro_Personas_Naturales_NP220636.Data
{
    public class PersonasDbContext : DbContext
    {
        public PersonasDbContext(DbContextOptions<PersonasDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
    }
}
