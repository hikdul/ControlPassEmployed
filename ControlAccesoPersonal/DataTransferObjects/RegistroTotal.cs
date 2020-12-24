using ControlAccesoPersonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.DataTransferObjects
{
    public class RegistroTotal
    {
        public int num { get; set; }
        public RegistroDiario registro { get; set; }
        public AdmoHorarios AdmoHorario { get; set; }
        public Horarios horario { get; set; }
        public Empleados Empleado { get; set; }
        public Persona persona { get; set; }
        public Empresa empresa { get; set; }
    }
}
