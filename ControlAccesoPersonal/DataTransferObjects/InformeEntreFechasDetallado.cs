using ControlAccesoPersonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.DataTransferObjects
{
    /// <summary>
    /// para devolver el informe entre fechas y anexarle una lista de los registros diarios estudiados
    /// </summary>
    public class InformeEntreFechasDetallado : InformeEntreFechas
    {
        /// <summary>
        /// lista de registros que se estudiaron en la elaboracion del informe
        /// </summary>
        public List<RegistroDiario> ListaRegistros { get; set; }
    }
}
