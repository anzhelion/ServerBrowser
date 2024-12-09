using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerBrowser.Data.Models
{
    public class ServerList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(128)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(32)]
        public string PublisherId { get; set; } = string.Empty!;

        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; } = null!;
        
        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int[] ServerIds { get; set; } = new int[100];
    }
}
