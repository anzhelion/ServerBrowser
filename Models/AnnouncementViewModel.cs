using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AnnouncementIdsStringMaxLen)]
        public string ServerIds { get; set; } = string.Empty;

        [Required]
        [MaxLength(AnnouncementTitleMaxLen)]
        [MinLength(AnnouncementTitleMinLen)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(AnnouncementDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }

        public int? Severity { get; set; }
    }
}
