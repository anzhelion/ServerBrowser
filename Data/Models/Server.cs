using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerBrowser.Data.Models
{
    public class Server
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(64)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(128)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(32)]
        public string PublisherId { get; set; } = string.Empty!;

        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; } = null!;
        
        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int ServerType { get; set; }
        // 0 - Diverse Server
        // 1 - Game Server
        // 2 - Lobby Server
        // 3 - Chatting Server

        [Required]
        public int IPAddress { get; set; }

        [Required]
        public DateTime LastLiveOn { get; set; }

        [Required]
        public int ActiveTimeStart { get; set; }

        [Required]
        public int ActiveTimeEnd { get; set; }

        [Required]
        public int PeakConcurrentUsers { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public bool IsDedicated {  get; set; }

        [Required]
        public bool IsOfficial { get; set; }

        [Required]
        public bool IsRemoved { get; set; }
    }
}
