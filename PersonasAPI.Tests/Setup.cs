using Microsoft.EntityFrameworkCore;
using Registro_Personas_Naturales_NP220636.Data;
using System;

namespace PersonasAPI.Tests
{
    public class Setup
    {
        public static PersonasDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PersonasDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PersonasDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
