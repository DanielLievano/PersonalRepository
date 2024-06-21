using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Domain.Models
{
    public class LogEntry
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public string Message { get; set; }
        [NotNull]
        public string Origin { get; set; }
        [NotNull]
        public String Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}
