using ControlAccesoPersonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.DataTransferObjects
{
    public class InformeEntreFechas
    {
        public InformeEntreFechas()
        {
            //ListaAdmoHorarios = new();
            //ListaRegistros = new();
            Mensajes = new();
        }

        public DateTime fechaInicioCalculo { get; set; }
        public DateTime fechaFinalCalculo { get; set; }


        public Persona Persona { get; set; }
        public Empleados Empleado { get; set; }
        public int HorasCumplidas { get; set; }
        public int extraMinutosCumplidos { get; set; }
        public int HorasDeberiaCumplir { get; set; }
        public int minutosTardanza { get; set; }
        public int minutosExtras { get; set; }
        public int DiaFaltados { get; set; }
        public int FeriadoTrabajados { get; set; }
        public int MarcasFaltantes { get; set; }
        public int DiasConRetrazoEntrada { get; set; }
        public int descanzos { get; set; }

        public List<string> Mensajes { get; set; }


        //public List<RegistroDiario> ListaRegistros { get; set; }
        //public List<Horarios> ListaHorarios { get; set; }
        //public List<AdmoHorarios> ListaAdmoHorarios { get; set; }





    }
}
