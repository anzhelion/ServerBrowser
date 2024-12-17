using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;
using System.Diagnostics.CodeAnalysis;

namespace ServerBrowser.Data.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(AnnouncementIdsMaxLen)]
        public int[] ServerIds { get; set; } = new int[AnnouncementIdsMaxLen];

        [MaxLength(AnnouncementTitleMaxLen)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(AnnouncementDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }

        public int? Severity { get; set; }
    }
}
