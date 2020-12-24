using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControlAccesoPersonal.Helpers;

namespace ControlAccesoPersonal.Models
{
    public class Empresa
    {

        public int id { get; set; }
        [RutCl]
        [Required]
        [Display(Name ="RUT")]
        [MaxLength(12)]
        public string rut { get; set; }
        [Required]
        [Display(Name = "Nombre De La Empresa")]
        [MaxLength(50)]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion De La Empresa")]
        [MaxLength(150)]
        public string description { get; set; }
}
}
