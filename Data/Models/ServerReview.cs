using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static ServerBrowser.Constants;

namespace ServerBrowser.Data.Models
{
    public class ServerReview
    {
        [Key]
        public int Id { get; set; }

        public int ServerId { get; set; }

        [MaxLength(ReviewTitleMaxLen)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(ReviewDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }
    }
}
