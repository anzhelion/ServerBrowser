using System.ComponentModel.DataAnnotations;

namespace ServerBrowser.Data.Models
{
    public class ServerType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(10)]
        [MaxLength(128)]
        public string Description { get; set; } = string.Empty;
    }
}
