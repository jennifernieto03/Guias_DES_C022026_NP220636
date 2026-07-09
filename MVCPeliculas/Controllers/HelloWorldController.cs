using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MVCPeliculas.Controllers
{
    public class HelloWorldController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*public string Index()
        {
            return "Esta es mi acción <b> predeterminada </b>";
        }*/

        // Modificación para el Ejercicio 1
        public IActionResult Welcome(string nombre, string apellido, int numVeces = 1)
        {
            ViewData["NombreCompleto"] = $"{nombre} {apellido}";
            ViewData["NumVeces"] = numVeces;

            return View();
        }

        public string Greeting(string nombre, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hola {nombre}, ID: {id}");
        }
    }
}
