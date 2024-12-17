using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static ServerBrowser.Constants;

namespace ServerBrowser.Data.Models
{
    public class ServerType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ServerTypeNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(ServerTypeDescMaxLength)]
        public string? Description { get; set; } = string.Empty;
    }
}
