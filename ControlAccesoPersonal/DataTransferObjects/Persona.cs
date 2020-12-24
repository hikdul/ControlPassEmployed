using ControlAccesoPersonal.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.DataTransferObjects
{
    /// <summary>
    /// este es el DTO que interactua directamente con el ciente mas no soporta ni tiene acceso directo a la base de datos
    /// </summary>
    public class Persona
    {
        public int id { get; set; }
        [Required]
        [RutCl]
        [Display(Name ="RUT")]
        [MaxLength(12)]
        public string rut { get; set; }
        [Required]
        [MaxLength(50)]
        // [esNombre]
        [Display(Name ="Primer Nombre")]
        public string nombre { get; set; }
        [Required]
        [MaxLength(50)]
        // [esNombre]
        [Display(Name = "Segundo Nombre")]
        public string nombre2 { get; set; }
        [Required]
        [MaxLength(50)]

        // [esNombre]
        [Display(Name = "Primer Apellido")]
        public string apellido { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string apellido2 { get; set; }
        [Required]
        [TelefonoCl]
        [Display(Name = "Telefono Mobil")]
        public string  telefono { get; set; }
        [Required]
        [CorreoElectronico]
        [Display(Name = "Correo Electronico")]
        public string correo { get; set; }

    }
}
