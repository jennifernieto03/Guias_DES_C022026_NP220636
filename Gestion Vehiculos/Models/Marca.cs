using System.Reflection;


namespace Gestion_Vehiculos.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
    }
}
