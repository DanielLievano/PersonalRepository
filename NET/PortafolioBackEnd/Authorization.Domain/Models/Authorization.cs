using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Authorization.Domain.Models
{
    public class Authorization
    {
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("NombreDeUsuario")]
        public string UserName { get; set; }
        [JsonPropertyName("Contraseña")]
        public string Password { get; set; }
    }
}
