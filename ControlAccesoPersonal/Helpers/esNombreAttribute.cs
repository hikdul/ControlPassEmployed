using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Helpers
{
    public class esNombreAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"> este trae el valor de la propiedad</param>
        /// <param name="validationContext"> este trae el contexto donde se va a validar</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            string patterm = @"/^[A-Za-z\s]+$/g";     //@"^[a - zA - Z ]+{ 2,150}";

            if (!Regex.IsMatch(value.ToString(), patterm))
                return new ValidationResult("Formato No Valido");

            return ValidationResult.Success;
        }

        // del siguiente modo es mas sencillo pero las repuesta de erroes son mas genericas

        public override bool IsValid(object value)
        {
            if (String.IsNullOrEmpty(value.ToString()))
                return true;

            string mail = @"/ ^([a - z0 - 9_\.-] +)@([\da - z\.-] +)\.([a - z\.]{ 2,6})$/";

            return Regex.IsMatch(value.ToString(), mail);
        }
    }
}
