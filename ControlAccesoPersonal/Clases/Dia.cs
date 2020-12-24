using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Clases
{
    /// <summary>
    /// hace referencia a las marchas de un dia en general
    /// </summary>
    public class Dia
    {

        public Dia()
        {
            DiaDeSemana = -1;
        }
        public Dia(int diaDeLaSemana)
        {
            DiaDeSemana = diaDeLaSemana;
        }

        public Dia(DateTime fecha)
        {
            DiaDeSemana = (int)fecha.DayOfWeek;
        }


        
            public int DiaDeSemana { get; set; }
        [Display(Name ="Hora Ingreso")]
            public string HoraI { get; set; }
        [Display(Name = "Hora Salida")]
        public string HoraS { get; set; }
        [Display(Name = "Hora Ingreso Del Descanzo")]
        public string HoraBI { get; set; }
        [Display(Name = "Hora Salida Al Descanzo")]
        public string HoraBS { get; set; }
        
    }
}
