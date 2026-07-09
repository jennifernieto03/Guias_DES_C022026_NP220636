using Microsoft.AspNetCore.Mvc;
using Persona.Models; 

namespace Persona.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            // Instanciamos la clase Persona con datos simulados
            var miPersona = new Models.Persona
            {
                DUI = "12345678-9",
                Nombre = "Juan",
                Apellido = "Pérez",
                FechaNacimiento = new DateTime(1995, 8, 20)
            };

            // Pasamos el objeto al modelo de la vista
            return View(miPersona);
        }
    }
}
