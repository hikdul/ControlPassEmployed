using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Helpers
{
    /// <summary>
    /// verifica si un elemento es un correo electronico valido
    /// </summary>
    public class CorreoElectronicoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;
            
            string mail = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";

            if (!Regex.IsMatch(value.ToString(), mail))
                return new ValidationResult("Por Favor Ingrese Un Email Valido");

            return ValidationResult.Success;

            //return base.IsValid(value, validationContext);
        }

      
    }
}
