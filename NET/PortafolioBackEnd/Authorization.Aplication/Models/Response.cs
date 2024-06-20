using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Authorization.Aplication.Models
{
    public class Response<T>
    {
        [JsonPropertyName("TimeStamp")]
        public string Timestamp { get; set; }
        [JsonPropertyName("Data")]
        public T Data { get; set; }

        public Response(T data)
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            Data = data;
        }
    }
}
