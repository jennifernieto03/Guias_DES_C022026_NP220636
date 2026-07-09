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

        public ActionResult Welcome(string nombre, int numVeces = 1)
        {
            ViewData["nombre"] = "Hola " + nombre;
            ViewData["numVeces"] = numVeces;
            return View();
        }

        public string Greeting(string nombre, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hola {nombre}, ID: {id}");
        }
    }
}
