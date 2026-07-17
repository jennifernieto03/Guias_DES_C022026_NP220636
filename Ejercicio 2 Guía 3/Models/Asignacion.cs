using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejercicio.Models
{
    public class Asignacion
    {
        public int Id { get; set; }

        [Required]
        public int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleado Empleado { get; set; }

        [Required]
        public int ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; }

        [Required]
        [StringLength(50)]
        public string Rol { get; set; }
    }
}
