
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime FechaContratacion { get; set; }

        [Required]
        [StringLength(50)]
        public string Puesto { get; set; }

        public List<Asignacion> Asignaciones { get; set; }
    }
}