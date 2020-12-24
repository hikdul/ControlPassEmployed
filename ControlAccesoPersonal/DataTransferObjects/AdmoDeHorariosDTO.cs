using ControlAccesoPersonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.DataTransferObjects
{
    public class AdmoDeHorariosDTO : AdmoHorarios
    {
        public List<RegistroDiario> RegistrosDiarios { get; set; }

    }
}
