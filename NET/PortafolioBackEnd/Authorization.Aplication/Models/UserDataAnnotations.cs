using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Authorization.Aplication.Models
{
    internal class UserDataAnnotations : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool existNumber = false;
            bool existUpper = false;
            bool existSimbol = false;
            Regex rg = new Regex(@"[^a-zA-Z0-9]");
            if (value == null)//No es nulo
                return false;
            if (value.ToString().Length < 8)//es mayor a 8 carecteres
                return false;
            foreach (var letra in value.ToString())//se busca un numero y una mayuscula
            {
                if (Char.IsNumber(letra))
                    existNumber = true;

                else if (Char.IsUpper(letra))
                    existUpper = true;
            }
            if (!existNumber)//contiene al menos un numero
                return false;

            if (!existUpper)//contiene al menos una mayuscula
                return false;

            if (!rg.IsMatch(value.ToString()))//contiene al menos un caracter especial
                return false;

            return true;
        }
    }
}
