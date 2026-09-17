using Microsoft.AspNetCore.Mvc;
using Registro_Personas_Naturales_NP220636.Data;
using Registro_Personas_Naturales_NP220636.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Registro_Personas_Naturales_NP220636.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly PersonasDbContext _context;

        public PersonasController(PersonasDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            // Validaciones de obligatoriedad y longitud (máximo 100 caracteres)
            if (string.IsNullOrWhiteSpace(persona.PrimerNombre) || persona.PrimerNombre.Length > 100)
                return BadRequest("El primer nombre es requerido y no debe exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(persona.PrimerApellido) || persona.PrimerApellido.Length > 100)
                return BadRequest("El primer apellido es requerido y no debe exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(persona.SegundoNombre) && persona.SegundoNombre.Length > 100)
                return BadRequest("El segundo nombre no debe exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(persona.SegundoApellido) && persona.SegundoApellido.Length > 100)
                return BadRequest("El segundo apellido no debe exceder 100 caracteres.");

            // Validación de Fecha de Nacimiento
            if (persona.FechaNacimiento == default(DateTime))
                return BadRequest("La fecha de nacimiento es requerida y debe ser válida.");

            // Validación del formato del DUI (01234567-8)
            if (string.IsNullOrWhiteSpace(persona.DUI) || !Regex.IsMatch(persona.DUI, @"^\d{8}-\d$"))
                return BadRequest("El formato del DUI es inválido. Debe ser 01234567-8.");

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            return persona;
        }
    }
}
