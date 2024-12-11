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
        [MaxLength(64)]
        public string PublisherId { get; set; } = string.Empty!;
        
        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int ServerType { get; set; }
        // 0 - No Server
        // 1 - Diverse Server
        // 2 - Game Server
        // 3 - Lobby Server
        // 4 - Chatting Server

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public DateTime LastLiveOn { get; set; }

        [Required]
        public DateTime ActiveTimeStart { get; set; }

        [Required]
        public DateTime ActiveTimeEnd { get; set; }

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
