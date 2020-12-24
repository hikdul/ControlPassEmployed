using ControlAccesoPersonal.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Models
{
    public class Horarios
    {

        public Horarios()
        {
            Lunes = new(1);
            Martes = new(2);
            Miercoles = new(3);
            Jueves = new(4);
            Viernes = new(5);
            Sabado = new(6);
            Domingo = new(0);

        }

        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public bool act { get; set; }
        public Dia Lunes { get; set; }
        public Dia Martes { get; set; }
        public Dia Miercoles { get; set; }
        public Dia Jueves { get; set; }
        public Dia Viernes { get; set; }
        public Dia Sabado { get; set; }
        public Dia Domingo { get; set; }



      
    }

    



}
