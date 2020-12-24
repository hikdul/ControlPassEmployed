using ControlAccesoPersonal.Clases;
using ControlAccesoPersonal.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Models
{

    /// <summary>
    /// aqui se almacenan los registro echos por  los empleados para llevar sus jornadas laborales
    /// </summary>
    public class RegistroDiario : Dia
    {
        public int id { get; set; }
        [Required]
        public int Empleado { get; set; }
        [Required]
        public DateTime fecha { get; set; }

    }
}
