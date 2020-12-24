using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Models
{
    public class Empleados
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int empresa { get; set; }
        [Required]
        public int persona { get; set; }
        [Display(Name ="salario")]
        [Required]
        public double sueldo { get; set; }
        [Display(Name = "Cargo")]
        public string cargo { get; set; }
        [Display(Name ="Articulo 22")]
        public bool articulo { get; set; }
    }
}
