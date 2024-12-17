using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class ServerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ServerTitleMinLen)]
        [MaxLength(ServerTitleMaxLen)]
        public string Title { get; set; } = string.Empty;

        [MinLength(ServerDescMinLen)]
        [MaxLength(ServerDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MinLength(ServerImageMinLen)]
        [MaxLength(ServerImageMaxLen)]
        public string? ImageUrl { get; set; } = string.Empty;

        [MinLength(PublisherMinLen)]
        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }

        public int? ServerType { get; set; }
        // 0 - Diverse Server
        // 1 - Game Server
        // 2 - Lobby Server
        // 3 - Chatting Server

        [MinLength(ServerAddressMinLen)]
        [MaxLength(ServerAddressMaxLen)]
        public string? IPAddress { get; set; }

        public DateTime? LastLiveOn { get; set; }

        public DateTime? ActiveTimeStart { get; set; }

        public DateTime? ActiveTimeEnd { get; set; }

        public int? PeakConcurrentUsers { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDedicated { get; set; }

        public bool IsOfficial { get; set; }

        public bool IsRemoved { get; set; }

        public string? TypeString { get; set; } = string.Empty;
    }
}
