using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlAccesoPersonal.Helpers
{
    public class TelefonoClAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

        if (String.IsNullOrEmpty(value.ToString()) || value == null)
            return ValidationResult.Success;

            string reg = @"\D*([+56]\d [2-9])(\D)(\d{4})(\D)(\d{4})\D*";

            if (Regex.IsMatch(value.ToString(), reg))
                return ValidationResult.Success;
            return new ValidationResult("Formato valido +56 X XXXX XXXX");

        }
    }
}
