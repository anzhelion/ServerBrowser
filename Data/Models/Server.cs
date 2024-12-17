using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;
using System.Diagnostics.CodeAnalysis;

namespace ServerBrowser.Data.Models
{
    public class Server
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ServerTitleMaxLen)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(ServerDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(ServerImageMaxLen)]
        public string? ImageUrl { get; set; } = string.Empty;
        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }

        public int? ServerType { get; set; }
        // 0 - No Server
        // 1 - Diverse Server
        // 2 - Game Server
        // 3 - Lobby Server
        // 4 - Chatting Server

        [MaxLength(ServerAddressMaxLen)]
        public string? IPAddress { get; set; }

        public DateTime? LastLiveOn { get; set; }

        public DateTime? ActiveTimeStart { get; set; }

        public DateTime? ActiveTimeEnd { get; set; }

        public int? PeakConcurrentUsers { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDedicated {  get; set; }

        public bool IsOfficial { get; set; }

        public bool IsRemoved { get; set; }
    }
}
