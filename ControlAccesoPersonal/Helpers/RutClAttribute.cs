using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Helpers
{
    public class RutClAttribute : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           

            if (String.IsNullOrEmpty(value.ToString()) || value == null)
                return ValidationResult.Success;

            string reg = @"^(\d{1,3}(?:\.\d{1,3}){2}-[\dkK])$";

            if (Regex.IsMatch(value.ToString(), reg))
                return ValidationResult.Success;

            return new ValidationResult("rut no valido || formato XX.XXX.XXX-k");

        }

    }
}
