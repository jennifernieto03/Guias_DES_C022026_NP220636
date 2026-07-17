
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio.Models
{
    public class Proyecto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        public List<Asignacion> Asignaciones { get; set; }
    }
}
