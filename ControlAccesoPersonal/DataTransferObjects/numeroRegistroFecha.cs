using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.DataTransferObjects
{
    /// <summary>
    /// se usa para retornar el dia y la cantidad de registros de ese dia
    /// </summary>
    public class numeroRegistroFecha
    {
        /// <summary>
        /// cantidad de registro por ese dia
        /// </summary>
        public int NumeroRegistro { get; set; }
        /// <summary>
        /// la fecha en la que se sabe el registro devuelto
        /// </summary>
        public DateTime fecha { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="num"></param>
        /// <param name="fe"></param>
        public numeroRegistroFecha(int num, DateTime fe)
        {
            this.NumeroRegistro = num;
            this.fecha = fe;
        }
    }
}
