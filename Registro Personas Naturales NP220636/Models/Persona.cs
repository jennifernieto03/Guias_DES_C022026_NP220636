namespace Registro_Personas_Naturales_NP220636.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string DUI { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
