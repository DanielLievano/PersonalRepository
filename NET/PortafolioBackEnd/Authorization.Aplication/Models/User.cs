using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Authorization.Aplication.Models
{
    public class User
    {
        [UserDataAnnotations(ErrorMessage ="Validar que {0} cumpla con los requisitos.")]
        [JsonPropertyName("Usuario")]
        public string UserName { get; set; }
        [JsonPropertyName("Contraseña")]
        public string Password { get; set; }
    }
}
