using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Models
{
    /// <summary>
    /// ESTAA ES UNA DE LAS CLASES PRINCIPALES YA QUE DICE A QUIEN LE TOCA UN HORARIO Y DU¿RANTE QUE  FECHAS ESTA ACTIVO
    /// </summary>
    public class AdmoHorarios
    {
        public int id { get; set; }
        [Required]
        public int Empleado { get; set; }
        [Required]
        public int horario { get; set; }
        [Required]
        public DateTime fechaInicio { get; set; }
        [Required]
        public DateTime fechaAcaba { get; set; }

    }
}
