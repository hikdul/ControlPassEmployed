using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Models
{
    /// <summary>
    /// contenedor de feriados
    /// </summary>
    public class Feriados
    {
        /// <summary>
        /// indica el identificador en base de datos
        /// </summary>
        [Key]
        public int id { get; set; }
        /// <summary>
        /// fecha a la cual esta ligada el feriado
        /// </summary>
        [Required(ErrorMessage = "La Fecha Es Obligatoria")]
        public DateTime fecha { get; set; }
        /// <summary>
        /// nombre que recibe el feriado
        /// </summary>
        [Required(ErrorMessage ="El Nombre es necesario")]
        [MaxLength(50,ErrorMessage ="la cantidad maxima de caracteres es 50")]
        public string nombre { get; set; }
        /// <summary>
        /// breve reseña sobre el feriado
        /// </summary>
        [Required(ErrorMessage = "La Descripcion es necesario")]
        [MaxLength(150)]
        public string description { get; set; }
        /// <summary>
        /// indica si es un feriado unico de esta epoca o si solo se cubre durante el año que fue introducido
        /// </summary>
        [Required(ErrorMessage = "Indique Si El Feriado Se Repite Anualmente")]
        public bool anual { get; set; }
    }
}
